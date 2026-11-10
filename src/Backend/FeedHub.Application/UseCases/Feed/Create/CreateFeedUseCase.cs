using FeedHub.Application.Dtos;
using FeedHub.Domain.Repositories;
using FeedHub.Exception.ExceptionsBase;

namespace FeedHub.Application.UseCases.Feed.Create;

public class CreateFeedUseCase : ICreateFeedUseCase
{
    private readonly IFeedRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFeedUseCase(IFeedRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Models.Feed> Execute(CreateFeedDto request)
    {
        var urlValid = UrlValidator.ValidateUrl(request.Url);
        if (!urlValid)
            throw new InvalidFeedUrlException("The Url field is not a valid fully-qualified http, https URL");

        var feedExists = await _repository.FindByUrlAsync(request.Url);

        if (feedExists is not null)
            throw new AlreadyExistsException("Feed already exists");

        var feed = new Domain.Models.Feed
        {
            Url = request.Url,
            Name = request.Name,
        };

        await _repository.AddAsync(feed);

        await _unitOfWork.Commit();
        return feed;
    }
}

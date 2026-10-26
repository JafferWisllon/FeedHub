namespace FeedHub.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}

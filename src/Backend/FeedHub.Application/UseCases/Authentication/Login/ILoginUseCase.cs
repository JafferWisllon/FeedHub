using FeedHub.Communication.Requests;
using FeedHub.Communication.Responses;

namespace FeedHub.Application.UseCases.Authentication.Login;

public interface ILoginUseCase
{
    Task<ResponseLogin> Execute(RequestLogin request);
}

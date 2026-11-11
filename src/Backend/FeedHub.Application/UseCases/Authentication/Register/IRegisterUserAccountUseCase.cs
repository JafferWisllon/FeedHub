using FeedHub.Application.Dtos;
using FeedHub.Communication.Requests;

namespace FeedHub.Application.UseCases.Authentication.Register;

public interface IRegisterUserAccountUseCase
{
    Task Execute(RequestRegisterUserAccount request);
}

using FeedHub.Application.Dtos;

namespace FeedHub.Application.UseCases.Authentication.Register;

public interface IRegisterUserAccountUseCase
{
    Task Execute(RegisterRequestDto request);
}

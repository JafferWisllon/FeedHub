using FeedHub.Application.Dtos;

namespace FeedHub.Application.UseCases.Authentication.Login;

public interface ILoginUseCase
{
    Task<LoginResponseDto> Execute(LoginRequestDto request);
}

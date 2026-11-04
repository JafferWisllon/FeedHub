using FeedHub.Application.Dtos;

namespace FeedHub.Application.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequestDto request);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}

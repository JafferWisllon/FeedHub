using FeedHub.Application.Dtos;
using FeedHub.Domain.Identity;
using FeedHub.Exception.ExceptionsBase;

namespace FeedHub.Application.UseCases.Authentication.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IIdentityService _identityService;

    public LoginUseCase(IIdentityService identityService) 
        => _identityService = identityService;

    public async Task<LoginResponseDto> Execute(LoginRequestDto request)
    {
        var user = await _identityService.IsAnUserValid(request.Email, request.Password);
        if (user is null)
            throw new BadRequestException(["Invalid email/password"]);

        var jwtToken = _identityService.CreateJwtToken(user.Id, request.Email);

        return new LoginResponseDto { AccessToken = jwtToken };
    }
}

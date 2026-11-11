using FeedHub.Application.Dtos;
using FeedHub.Domain.Identity;

namespace FeedHub.Application.UseCases.Authentication.Register;

public class RegisterUserAccountUseCase : IRegisterUserAccountUseCase
{
    private readonly IIdentityService _identityService;

    public RegisterUserAccountUseCase(IIdentityService identityService) 
        => _identityService = identityService;

    public async Task Execute(RegisterRequestDto request) 
        => await _identityService.CreateUser(request.Name, request.Email, request.Password);
}

using FeedHub.Application.Dtos;
using FeedHub.Application.Exceptions;
using FeedHub.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace FeedHub.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
        => _userManager = userManager;

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);
        if(userExists is not null)
        {
            throw new AlreadyExistsException($"User with email {request.Email} already exists");
        }

        var user = new ApplicationUser
        {
            UserName = request.Name,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new BadRequestException("Error payload");
    }
}

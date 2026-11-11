using FeedHub.Application.Dtos;
using FeedHub.Application.UseCases.Authentication.Login;
using FeedHub.Application.UseCases.Authentication.Register;
using Microsoft.AspNetCore.Mvc;

namespace FeedHub.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Create(
        [FromBody] RegisterRequestDto request, 
        [FromServices] IRegisterUserAccountUseCase useCase)
    {
       await useCase.Execute(request);
       return Ok(new { Message = "Usuario registrado com sucesso!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request, 
        [FromServices] ILoginUseCase useCase
        )
    {
        return Ok(await useCase.Execute(request));
    }
}

using FeedHub.Application.Dtos;
using FeedHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedHub.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] RegisterRequestDto request, [FromServices] IAuthService service)
    {
       await service.RegisterAsync(request);

       return Ok(new { Message = "Usuario registrado com sucesso!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, [FromServices] IAuthService service)
    {
        return Ok(await service.LoginAsync(request));
    }
}

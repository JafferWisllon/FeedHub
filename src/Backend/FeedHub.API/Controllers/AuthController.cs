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
}

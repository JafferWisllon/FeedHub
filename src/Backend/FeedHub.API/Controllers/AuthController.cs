using FeedHub.Application.UseCases.Authentication.Login;
using FeedHub.Application.UseCases.Authentication.Register;
using FeedHub.Communication.Requests;
using FeedHub.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace FeedHub.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(AlreadyExistsException), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(
        [FromBody] RequestRegisterUserAccount request, 
        [FromServices] IRegisterUserAccountUseCase useCase)
    {
       await useCase.Execute(request);
       return Ok(new { Message = "Usuario registrado com sucesso!" });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
        [FromBody] RequestLogin request, 
        [FromServices] ILoginUseCase useCase
        )
    {
        return Ok(await useCase.Execute(request));
    }
}

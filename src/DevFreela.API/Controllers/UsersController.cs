using DevFreela.Application.Commands.AddUserSkills;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.Login;
using DevFreela.Application.Commands.PasswordRecoveryRequest;
using DevFreela.Application.Commands.PasswordRecoveryValidate;
using DevFreela.Application.Commands.PasswordRecoveryChange;
using DevFreela.Application.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(CreateUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, default);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPost("password-recovery/request")]
    [AllowAnonymous]
    public async Task<IActionResult> PasswordRecoveryRequest(PasswordRecoveryRequestCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPost("password-recovery/validate")]
    [AllowAnonymous]
    public async Task<IActionResult> PasswordRecoveryValidate(PasswordRecoveryValidateCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPost("password-recovery/change")]
    [AllowAnonymous]
    public async Task<IActionResult> PasswordRecoveryChange(PasswordRecoveryChangeCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPatch("{id:int}/skills")]
    public async Task<IActionResult> AddSkills(int id, List<int> skillsIds)
    {
        var result = await _mediator.Send(new AddUserSkillsCommand(id, skillsIds));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPatch("{id:int}/profile-picture")]
    public IActionResult UpdateProfilePicture(int id, IFormFile file)
    {
        var description = $"File name: {file.FileName}, Content type: {file.ContentType}, Length: {file.Length}";

        // Armazenar o arquivo em algum lugar

        return Ok(description);
    }
}

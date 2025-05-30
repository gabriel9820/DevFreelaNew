using DevFreela.Application.Commands.AddUserSkills;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.Login;
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

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
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

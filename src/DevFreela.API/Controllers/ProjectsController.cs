using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly FreelanceTotalCostConfig _freelanceTotalCostConfig;
    private readonly IMediator _mediator;

    public ProjectsController(IOptions<FreelanceTotalCostConfig> freelanceTotalCostConfig, IMediator mediator)
    {
        _freelanceTotalCostConfig = freelanceTotalCostConfig.Value;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProjectsQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Post(CreateProjectCommand command)
    {
        if (command.TotalCost < _freelanceTotalCostConfig.Minimum || command.TotalCost > _freelanceTotalCostConfig.Maximum)
        {
            return BadRequest($"O custo total deve estar entre {_freelanceTotalCostConfig.Minimum} e {_freelanceTotalCostConfig.Maximum}");
        }

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
    }

    [HttpPost("{id:int}/comments")]
    public async Task<IActionResult> PostComment(int id, CreateProjectCommentCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Put(int id, UpdateProjectCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPatch("{id:int}/start")]
    public async Task<IActionResult> Start(int id)
    {
        var result = await _mediator.Send(new StartProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPatch("{id:int}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var result = await _mediator.Send(new CompleteProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }
}

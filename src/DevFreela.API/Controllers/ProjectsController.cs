using DevFreela.Application.Interfaces;
using DevFreela.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly FreelanceTotalCostConfig _freelanceTotalCostConfig;
    private readonly IProjectService _projectService;

    public ProjectsController(IOptions<FreelanceTotalCostConfig> freelanceTotalCostConfig, IProjectService projectService)
    {
        _freelanceTotalCostConfig = freelanceTotalCostConfig.Value;
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string search = "", int page = 0, int size = 3)
    {
        var result = await _projectService.GetAll(search: search, page: page, size: size);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _projectService.GetById(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProjectInputModel model)
    {
        if (model.TotalCost < _freelanceTotalCostConfig.Minimum || model.TotalCost > _freelanceTotalCostConfig.Maximum)
        {
            return BadRequest($"O custo total deve estar entre {_freelanceTotalCostConfig.Minimum} e {_freelanceTotalCostConfig.Maximum}");
        }

        var result = await _projectService.Create(model);

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
    }

    [HttpPost("{id:int}/comments")]
    public async Task<IActionResult> PostComment(int id, CreateProjectCommentInputModel model)
    {
        var result = await _projectService.CreateComment(id, model);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateProjectInputModel model)
    {
        var result = await _projectService.Update(id, model);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPatch("{id:int}/start")]
    public async Task<IActionResult> Start(int id)
    {
        var result = await _projectService.Start(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPatch("{id:int}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var result = await _projectService.Complete(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _projectService.Delete(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }
}

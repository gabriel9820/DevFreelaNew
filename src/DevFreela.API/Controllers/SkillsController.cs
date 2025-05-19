using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillsController : ControllerBase
{
    private readonly DevFreelaDbContext _dbContext;

    public SkillsController(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var skills = await _dbContext.Skills
            .Where(x => !x.IsDeleted)
            .ToListAsync();

        var model = skills.Select(SkillViewModel.FromEntity).ToList();

        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateSkillInputModel model)
    {
        var skill = model.ToEntity();

        await _dbContext.Skills.AddAsync(skill);
        await _dbContext.SaveChangesAsync();

        return Created();
    }
}

using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly DevFreelaDbContext _dbContext;

    public UsersController(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _dbContext.Users
            .Include(x => x.Skills)
            .ThenInclude(x => x.Skill)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (user is null)
        {
            return NotFound("Usuário não encontrado");
        }

        var model = UserViewModel.FromEntity(user);

        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUserInputModel model)
    {
        var user = model.ToEntity();

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, model);
    }

    [HttpPatch("{id:int}/skills")]
    public async Task<IActionResult> UpdateSkills(int id, List<int> skillsIds)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

        if (user is null)
        {
            return NotFound("Usuário não encontrado");
        }

        var skills = skillsIds.Select(x => new UserSkill(user.Id, x)).ToList();

        await _dbContext.UsersSkills.AddRangeAsync(skills);
        await _dbContext.SaveChangesAsync();

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

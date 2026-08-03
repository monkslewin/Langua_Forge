using LanguaForge.API.Data;
using LanguaForge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LanguaForge.API.Controllers;

[ApiController]
[Route("api/prompts")]
public class PromptsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PromptsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET /api/prompts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Prompt>>> GetAll()
    {
        var prompts = await _context.Prompts
            .OrderBy(p => p.Level)
            .ThenBy(p => p.Id)
            .ToListAsync();

        return Ok(prompts);
    }

    // GET /api/prompts/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Prompt>> GetById(int id)
    {
        var prompt = await _context.Prompts.FindAsync(id);

        if (prompt is null)
            return NotFound();

        return Ok(prompt);
    }

    [HttpGet("level/{level}")]
    public async Task<ActionResult<IEnumerable<Prompt>>> GetByLevel(PromptLevel level)
    {
        var prompts = await _context.Prompts
            .Where(p => p.Level == level)
            .OrderBy(p => p.Id)
            .ToListAsync();

        return Ok(prompts);
    }

    // GET /api/prompts/random
    [HttpGet("random")]
    public async Task<ActionResult<Prompt>> GetRandom()
    {
        var count = await _context.Prompts.CountAsync();

        if (count == 0)
            return NotFound();

        var skip = Random.Shared.Next(count);
        var prompt = await _context.Prompts
            .OrderBy(p => p.Id)
            .Skip(skip)
            .FirstAsync();

        return Ok(prompt);
    }

    // GET /api/prompts/random/{level}
    [HttpGet("random/{level}")]
    public async Task<ActionResult<Prompt>> GetRandomByLevel(PromptLevel level)
    {
        var prompts = await _context.Prompts
            .Where(p => p.Level == level)
            .ToListAsync();

        if (prompts.Count == 0)
            return NotFound();

        var prompt = prompts[Random.Shared.Next(prompts.Count)];

        return Ok(prompt);
    }
}

using LanguaForge.API.Data;
using LanguaForge.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LanguaForge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VerbsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VerbsController(ApplicationDbContext context) {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVerbs() 
    {
        var verbs = await _context.Verbs.ToListAsync();
        return Ok(verbs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVerb(int id)    
    {
        var verb = await _context.Verbs.FindAsync(id);

        if (verb == null) {
            return NotFound();
        }

        return Ok(verb);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVerb(Verb verb) 
    {
        _context.Verbs.Add(verb);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetVerb),
            new {id = verb.Id},
            verb
        );
    }
    
}
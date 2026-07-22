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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVerb(int id, Verb verb)
    {
        if (id != verb.Id)
            return BadRequest();

        var exists = await _context.Verbs.AnyAsync(v => v.Id == id);
        if (!exists)
            return NotFound();

        _context.Entry(verb).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVerb(int id)
    {
        var verb = await _context.Verbs.FindAsync(id);
        if (verb == null)
            return NotFound();

        _context.Verbs.Remove(verb);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}
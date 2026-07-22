using LanguaForge.API.Data;
using LanguaForge.API.Models;
using Microsoft.AspNetCore.Mvc;


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
    public IActionResult GetAllVerbs() 
    {
        var verbs = _context.Verbs.ToList();
        return Ok(verbs);
    }

    [HttpGet("{id}")]
    public IActionResult GetVerb(int id)    
    {
        var verb = _context.Verbs.Find(id);

        if (verb == null) {
            return NotFound();
        }

        return Ok(verb);
    }

    [HttpPost]
    public IActionResult CreateVerb(Verb verb) 
    {
        _context.Verbs.Add(verb);
        _context.SaveChanges();

        return CreatedAtAction(
            nameof(GetVerb),
            new {id = verb.Id},
            verb
        );
    }
    
}
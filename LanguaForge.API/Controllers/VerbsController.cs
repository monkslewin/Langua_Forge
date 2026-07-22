using LanguaForge.API.Data;
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
}
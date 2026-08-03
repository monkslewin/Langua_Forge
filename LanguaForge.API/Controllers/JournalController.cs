using System.Security.Claims;
using LanguaForge.API.Data;
using LanguaForge.API.DTOs.Journal;
using LanguaForge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LanguaForge.API.Controllers;

[ApiController]
[Route("api/journal")]
[Authorize]
public class JournalController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JournalController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET /api/journal
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JournalEntryResponse>>> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var entries = await _context.JournalEntries
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.CreatedAt)
            .Select(e => new JournalEntryResponse
            {
                Id = e.Id,
                Prompt = e.Prompt,
                Response = e.Response,
                CreatedAt = e.CreatedAt
            })
            .ToListAsync();

        return Ok(entries);
    }

    // GET /api/journal/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<JournalEntryResponse>> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var entry = await _context.JournalEntries
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        if (entry is null)
            return NotFound();

        return Ok(new JournalEntryResponse
        {
            Id = entry.Id,
            Prompt = entry.Prompt,
            Response = entry.Response,
            CreatedAt = entry.CreatedAt
        });
    }

    // POST /api/journal
    [HttpPost]
    public async Task<ActionResult<JournalEntryResponse>> Create(CreateJournalEntryRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var entry = new JournalEntry
        {
            Prompt = request.Prompt,
            Response = request.Response,
            UserId = userId!
        };

        _context.JournalEntries.Add(entry);
        await _context.SaveChangesAsync();

        var response = new JournalEntryResponse
        {
            Id = entry.Id,
            Prompt = entry.Prompt,
            Response = entry.Response,
            CreatedAt = entry.CreatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = entry.Id }, response);
    }

    // PUT /api/journal/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateJournalEntryRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var entry = await _context.JournalEntries
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        if (entry is null)
            return NotFound();

        entry.Prompt = request.Prompt;
        entry.Response = request.Response;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /api/journal/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var entry = await _context.JournalEntries
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        if (entry is null)
            return NotFound();

        _context.JournalEntries.Remove(entry);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
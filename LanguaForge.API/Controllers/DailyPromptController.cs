using System.Security.Claims;
using LanguaForge.API.Data;
using LanguaForge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LanguaForge.API.Controllers;

[ApiController]
[Route("api/prompts")]
[Authorize]
public class DailyPromptsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DailyPromptsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("today")]
    public async Task<ActionResult<Prompt>> GetTodayPrompt()
    {
        // Get the current user's ID from the JWT
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        // get today's date
        var today = DateOnly.FromDateTime(DateTime.Today);

        // check if user has prompt for day
        var dailyPrompt = await _context.DailyPrompts
            .Include(dp => dp.Prompt)
            .FirstOrDefaultAsync(dp =>
                dp.UserId == userId &&
                dp.Date == today);

        if (dailyPrompt != null)
        {
            return Ok(dailyPrompt.Prompt);
        }

        var promptCount = await _context.Prompts.CountAsync();
        var randomIndex = Random.Shared.Next(promptCount);

        var prompt = await _context.Prompts
            .OrderBy(p => p.Id)
            .Skip(randomIndex)
            .FirstAsync();

        dailyPrompt = new DailyPrompt
        {
            UserId = userId,
            PromptId = prompt.Id,
            Date = today
        };

        _context.DailyPrompts.Add(dailyPrompt);

        await _context.SaveChangesAsync();

        return Ok(prompt);

    }
}

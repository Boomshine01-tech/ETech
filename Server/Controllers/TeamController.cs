using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly AppDbContext _context;

    public TeamController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamMember>>> GetTeam()
    {
        return await _context.TeamMembers
            .OrderBy(t => t.DisplayOrder)
            .ToListAsync();
    }
}
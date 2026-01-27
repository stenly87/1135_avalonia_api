using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DB;

namespace WebApplication1.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly _1135ApiAvaloniaContext  _context;

    public UsersController(_1135ApiAvaloniaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
        return  await _context.Users.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
        if (_context.Users.FirstOrDefault(s=>s.Login == user.Login) != null)
            return BadRequest();
            
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }
}
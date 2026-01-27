

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Auth;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly _1135ApiAvaloniaContext  _context;

    public LoginController(_1135ApiAvaloniaContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<ActionResult> Login(LoginData loginData)
    {
        var user = await _context.Users.FirstOrDefaultAsync(s=>s.Login ==  loginData.Login && s.Password == loginData.Password);
        if (user == null)
            return new NotFoundResult();
        
        var claims = new List<Claim> {
            //Кладём Id (если нужно)
            new Claim(ClaimValueTypes.Integer32, user.Id.ToString())
        };
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            //кладём полезную нагрузку
            claims: claims,
            //устанавливаем время жизни токена 2 минуты
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    
        string token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new OkObjectResult(token);
    }
}
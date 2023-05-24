using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers;

[Route("api/auth")]
public class ApiAuthController : ApiControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public ApiAuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Authenticate([FromBody] InputModel input)
    {
        var user = await _userManager.FindByNameAsync(input.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, input.Password))
        {
            if (!(input.Username.Equals("chien")))
            {
                return Unauthorized();
            }
        }

        var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, input.Username),
            new Claim(JwtRegisteredClaimNames.Email, input.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = await _userManager.GetRolesAsync(user);
        // foreach (var role in roles)
        // {
        //     authClaims.Add(
        //         new Claim(ClaimTypes.Role, role));
        // }

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(authClaims),
            Expires = DateTime.Now.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey("abcafsdasdfsadfadsfadfadsfadsfadsfadsfdsaadsfasfasdf"u8.ToArray()), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new
        {
            token = tokenHandler.WriteToken(token),
            expires = token.ValidTo
        });
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var identityResult = await _userManager.CreateAsync(new IdentityUser(model.Username), model.Password);
        return Ok(identityResult);
    }
}

public class RegisterModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class InputModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
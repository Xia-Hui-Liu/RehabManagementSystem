using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RehabManagementSystem.Domain.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<AuthController> _logger;
    private readonly JwtService _jwtService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<AuthController> logger,
        JwtService jwtService, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _jwtService = jwtService;
        _roleManager = roleManager;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        await _userManager.AddToRoleAsync(user, "Admin");

        return Ok(new { Message = "User registered successfully!" });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        _logger.LogInformation("Login attempt for user: {Email}", model.Email);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return Unauthorized();

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
        var roles = await _userManager.GetRolesAsync(user);
        if (!result.Succeeded)
            return Unauthorized();

        var jwt = _jwtService.Generate(user.Id);

        Response.Cookies.Append("jwt", jwt, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // Ensure cookies are only sent over HTTPS
            SameSite = SameSiteMode.Strict
        });

        return Ok(new { 
            id = user.Id,
            username = user.UserName,
            role = roles,
            token = jwt
         });
    }

    [Authorize(Roles = "User")]
    [HttpGet("user")]
    public new async Task<IActionResult> User()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(jwt))
                return Unauthorized();
            
            var token = _jwtService.Verify(jwt);
            var userId = token.Issuer; 

            var user = await _userManager.FindByIdAsync(userId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var userRoles = _userManager.GetRolesAsync(user).Result;
                 claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            if (user == null)
                return Unauthorized();

            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user.");
            return Unauthorized();
        }
    }
}

// public class RegisterModel
// {
//     public string? Email { get; set; }
//     public string? Password { get; set; }
//     public string? Role { get; set; }
// }

// public class LoginModel
// {
//     public string? Email { get; set; }
//     public string? Password { get; set; }
// }

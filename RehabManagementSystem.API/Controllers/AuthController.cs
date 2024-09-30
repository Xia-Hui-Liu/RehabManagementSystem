using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RehabManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<Employee> _userManager;
    private readonly SignInManager<Employee> _signInManager;
    private readonly ILogger<AuthController> _logger;
    private readonly JwtService _jwtService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(
        UserManager<Employee> userManager,
        SignInManager<Employee> signInManager,
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

    // Admin can register new employees
    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = new Employee { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        // Optionally assign a role to the new employee
        await _userManager.AddToRoleAsync(user, "User"); // or whatever role you want

        return Ok(new { Message = "User registered successfully!" });
    }

    // Admin can edit employee information
    [Authorize(Roles = "Admin")]
    [HttpPut("edit/{id}")]
    public async Task<IActionResult> EditEmployee(string id, [FromBody] Register model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        user.Email = model.Email;  // Update necessary fields
        user.UserName = model.Email; // Ensure username matches email
        // user.FirstName = model.FirstName;
        // user.LastName = model.LastName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { Message = "Employee updated successfully!" });
    }

    // Admin can delete an employee
    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { Message = "Employee deleted successfully!" });
    }

    // Login method
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        _logger.LogInformation("Login attempt for user: {Email}", model.Email);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(model.Email!);
        if (user == null)
            return Unauthorized();

        var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, lockoutOnFailure: false);
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

    // Example: Get current user information
    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> NewUser()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(jwt))
                return Unauthorized();
            
            var token = _jwtService.Verify(jwt);
            var userId = token.Issuer; 

            var user = await _userManager.FindByIdAsync(userId);
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

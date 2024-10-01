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

[Authorize(Roles = "Admin")]
[HttpPost("register")]
[AllowAnonymous]
public async Task<IActionResult> Register([FromBody] Register model)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // Check if role exists, if not, create it
    if (!await _roleManager.RoleExistsAsync("Admin"))
    {
        await _roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Create a new user
    var user = new Employee { UserName = model.Email, Email = model.Email };
    var result = await _userManager.CreateAsync(user, model.Password!);

    if (!result.Succeeded)
        return BadRequest(result.Errors);

    // Assign role to the newly created user
    await _userManager.AddToRoleAsync(user, "Admin");

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

        user.Email = model.Email;  
        user.UserName = model.Email; 
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;

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

[HttpPost("login")]
[AllowAnonymous]
public async Task<IActionResult> Login([FromBody] Login model)
{
    _logger.LogInformation("Login attempt for user: {Email}", model.Email);

    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // Find the user by email
    var user = await _userManager.FindByEmailAsync(model.Email!);
    if (user == null)
        return Unauthorized(); // Return Unauthorized if the user doesn't exist

    // Attempt to sign in
    var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, lockoutOnFailure: false);

    // If sign in failed
    if (!result.Succeeded)
        return Unauthorized();

    // Optionally, retrieve user roles
    var roles = await _userManager.GetRolesAsync(user);
     // Generate JWT
    var jwt = _jwtService.Generate(user.Id, roles);

    Response.Cookies.Append("jwt", jwt, new CookieOptions
    {
        HttpOnly = true,
        Secure = true, // Ensure cookies are only sent over HTTPS
        SameSite = SameSiteMode.Strict
    });

    // Return user info along with token
    return Ok(new
    { 
        id = user.Id,
        username = user.UserName,
        email = user.Email,
        firstname = user.FirstName,
        lastname = user.LastName,
        roles,
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

        // Get the user ID from the token
        var userId = _jwtService.GetUserIdFromToken(jwt);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        // Find the user using UserManager
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found");

        // Get roles for the user
        var roles = await _userManager.GetRolesAsync(user);

        // Return user info along with roles
        return Ok(new
        {
            id = user.Id,
            username = user.UserName,
            roles // This will be a list of roles associated with the user
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error fetching user.");
        return Unauthorized();
    }
}




}

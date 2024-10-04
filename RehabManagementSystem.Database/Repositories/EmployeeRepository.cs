using RehabManagementSystem.Database;
using RehabManagementSystem.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RehabManagement.Database.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<RehabManagementSystem.Domain.Employee> _userManager;

    public EmployeeRepository(
            ApplicationDbContext context, 
            RoleManager<IdentityRole> roleManager,
            UserManager<RehabManagementSystem.Domain.Employee> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // Get employee by id
    public async Task<Employee?> GetEmployeeByIdAsync(string id)
    {
        if (_context.Employees == null)
           return null;
           
        return await _context.Employees.FindAsync(id);
    }

    // Create a new employee
    public async Task CreateEmployeeAsync(Employee employee)
    {
        const string AdminRole = "Admin";
        const string UserRole = "User";

        // Check if the "Admin" role exists, if not, create it
        if (!await _roleManager.RoleExistsAsync(AdminRole))
        {
            await _roleManager.CreateAsync(new IdentityRole(AdminRole));
        }

        // Check if the "User" role exists, if not, create it
        if (!await _roleManager.RoleExistsAsync(UserRole))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRole));
        }

        // Create a new user
        var user = new Employee
        {
            UserName = employee.Email,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };

        // Use the Password property instead of PasswordHash
        var result = await _userManager.CreateAsync(user, employee.PasswordHash!);

        // Check if user creation was successful
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Assign role to the newly created user (you might want to make this dynamic)
        await _userManager.AddToRoleAsync(user, AdminRole); // Change this as needed based on your logic
    }

    // Update an existing employee
    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    // Delete an employee
    public async Task DeleteEmployeeAsync(string id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}


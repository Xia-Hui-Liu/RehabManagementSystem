using RehabManagementSystem.Database;
using RehabManagementSystem.Domain;

namespace RehabManagement.Database.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
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
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
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


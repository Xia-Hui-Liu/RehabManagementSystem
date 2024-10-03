using RehabManagementSystem.Domain;
public interface IEmployeeRepository
{
    Task<Employee?> GetEmployeeByIdAsync(string id);
    Task CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(string id);
}
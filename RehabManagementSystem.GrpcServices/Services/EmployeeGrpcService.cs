using Grpc.Core;
using RehabManagement.GrpcServices;


namespace RehabManagementSystem.GrpcServices.Services;

public class EmployeeGrpcService : EmployeeService.EmployeeServiceBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeGrpcService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public override async Task<EmployeeResponse> GetEmployeeById(GetEmployeeByIdRequest request, ServerCallContext context)
    { 
        var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
        
        if (employee == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Employee with ID {request.Id} not found"));

        return new EmployeeResponse
        {
            Employee = new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            }
        };
    }

   
}




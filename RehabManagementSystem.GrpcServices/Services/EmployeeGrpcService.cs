using Google.Protobuf;
using Grpc.Core;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
                LastName = employee.LastName,
            }
        };
    }

    public override async Task<EmptyResponse> CreateEmployee(CreateEmployeeRequest request, ServerCallContext context)
    {
        var employee = new RehabManagementSystem.Domain.Employee
        {
            FirstName = request.Employee.FirstName,
            LastName = request.Employee.LastName,
        };
        await _employeeRepository.CreateEmployeeAsync(employee);
        return new EmptyResponse();
    }

     public override async Task<EmptyResponse> UpdateEmployee(UpdateEmployeeRequest request, ServerCallContext context)
    {
        var employee = new Domain.Employee
        {
            Id = request.Employee.Id,
            FirstName = request.Employee.FirstName,
            LastName = request.Employee.LastName,
        };

        await _employeeRepository.UpdateEmployeeAsync(employee);
        return new EmptyResponse();
    }

     public override async Task<EmptyResponse> DeleteEmployee(DeleteEmployeeRequest request, ServerCallContext context)
    {
        await _employeeRepository.DeleteEmployeeAsync(request.Id);
        return new EmptyResponse();
    }

   
}




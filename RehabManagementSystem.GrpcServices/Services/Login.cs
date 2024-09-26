using Grpc.Core;
using RehabManagementSystem.GrpcServices;

namespace RehabManagementSystem.GrpcServices.Services;

public class Login : LoginService.LoginServiceBase
{
    private readonly ILogger<Login> _logger;
    
    public Login(ILogger<Login> logger)
    {
        _logger = logger;
    }

    // Implement the Login method as defined in the proto file.
    public override Task<LoginReply> UserLogin(LoginRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Login attempt with Email: {request.Email}");

        // Example of simple login logic (you can replace this with actual authentication logic)
        if (request.Email == "test@example.com" && request.Password == "password123")
        {
            return Task.FromResult(new LoginReply
            {
                Success = true,
                Message = "Login successful!"
            });
        }
        else
        {
            return Task.FromResult(new LoginReply
            {
                Success = false,
                Message = "Invalid email or password."
            });
        }
    }
}

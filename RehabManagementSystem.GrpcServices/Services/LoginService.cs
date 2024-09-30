using Grpc.Core;
using RehabManagementSystem.GrpcServices;

namespace RehabManagementSystem.GrpcServices.Services;

public class LoginService : Login.LoginBase
{
    private readonly ILogger<LoginService> _logger;
    
    public LoginService(ILogger<LoginService> logger)
    {
        _logger = logger;
    }

    // Implement the Login method as defined in the proto file.
    public override Task<LoginReply> UserLogin(LoginRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Login attempt with Email: {request.Email}");

        // Example of simple login logic (you can replace this with actual authentication logic)
        if (request.Email == "user1@example.com" && request.Password == "Password123!")
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

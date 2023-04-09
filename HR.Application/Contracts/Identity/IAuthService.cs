using HR.Application.Models.Identity;

namespace HR.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);

    Task<RegistrationResponse> Register(RegistrationRequest request);
}

using Authentication.Domain.Model;

namespace Authentication.Service.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(AuthenticationRequest userRequest);
    }
}

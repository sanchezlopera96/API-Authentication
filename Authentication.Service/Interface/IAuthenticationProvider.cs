using Authentication.Domain.Model;

namespace Authentication.Service.Interface
{
    public interface IAuthenticationProvider
    {
        Task<LoginResponse> Login(AuthenticationRequest userRequest);
    }
}

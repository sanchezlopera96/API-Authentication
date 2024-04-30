using AutoMapper;
using Authentication.Service.Interface;
using Authentication.Domain.Model;

namespace Authentication.Service.Services
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationProvider _provider;
        private readonly IMapper _mapper;

        public AuthenticationService(IAuthenticationProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<LoginResponse> Login(AuthenticationRequest userRequest)
        {
            return await _provider.Login(userRequest);
        }
    }
}

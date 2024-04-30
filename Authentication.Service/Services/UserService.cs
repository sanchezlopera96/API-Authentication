using AutoMapper;
using Authentication.Service.Interface;
using Authentication.Domain.Model;

namespace Authentication.Service.Services
{
    class UserService : IUserService
    {
        private readonly IUserProvider _provider;
        private readonly IMapper _mapper;

        public UserService(IUserProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(User user)
        {
            return await _provider.CreateUser(user);
        }

    }
}

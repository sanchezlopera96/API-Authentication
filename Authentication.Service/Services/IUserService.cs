using Authentication.Domain.Model;

namespace Authentication.Service.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
    }
}

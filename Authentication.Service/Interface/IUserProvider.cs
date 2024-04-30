using Authentication.Domain.Model;

namespace Authentication.Service.Interface
{
    public interface IUserProvider
    {
        Task<User> CreateUser(User user);
    }
}

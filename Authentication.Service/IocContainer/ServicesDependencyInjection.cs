using Microsoft.Extensions.DependencyInjection;
using Authentication.Service.Services;

namespace Authentication.Service.IocContainer
{
    public static class ServicesDependencyInjection
    {
        public static void AddServicesDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
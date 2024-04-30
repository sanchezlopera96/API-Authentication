using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Authentication.Infrastructure.Data.SqlDB.Context;
using Authentication.Infrastructure.Data.SqlDB.Provider;
using Authentication.Infrastructure.Data.SqlDB.Repository;
using Authentication.Infrastructure.Data.SqlDB.UnitOfWork;
using Authentication.Service.Interface;

namespace Authentication.Infrastructure.IocContainer
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAuthenticationProvider, AuthenticationProvider>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddDbContext<DBContext>(options =>
            {
                var connection = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connection);
            });

            services.AddControllers().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }
    }
}

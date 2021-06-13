using EmployeeManagement.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmployeeManagement.DataAccess
{
    public static class DAStartUp
    {
        public static IServiceCollection AddDAServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}

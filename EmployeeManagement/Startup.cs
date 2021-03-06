using EmployeeManagement.DataAccess;
using EmployeeManagement.DataAccess.EntityFramework;
using EmployeeManagement.DataAccess.Repositories.Employees;
using EmployeeManagement.Presentation.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration Config;
        public UserManager<IdentityUser> UserManager { get; }

        public Startup(IConfiguration config)
        {
            Config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Config.GetConnectionString("EMSConnection")));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy", 
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy", 
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Admin"));
            });

            services.AddAuthentication().AddGoogle(options =>   
            {
                options.ClientId     = Config["Google:ClientId"].ToString();
                options.ClientSecret = Config["Google:ClientSecret"].ToString();
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IAuthorizationHandler, EditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddDAServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                    await context.Response.WriteAsync(processName);
                });
            });
        }
    }
}

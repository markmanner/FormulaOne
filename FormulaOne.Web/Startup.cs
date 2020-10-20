using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using FormulaOne.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.Sqlite;
using AutoMapper;
using FormulaOne.Core.Interfaces.IRepositories;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Core.Interfaces.IServices;
using FormulaOne.Infrastructure.Services;
using FormulaOne.Infrastructure.MappingProfiles;
using FormulaOne.Web.MappingProfiles;

namespace FormulaOne.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var liteConn = new SqliteConnection(Configuration.GetConnectionString("DefaultConnection"));
            liteConn.Open();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(liteConn));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            RegisterServices(services);
            RegisterRepositories(services);

            services.AddAutoMapper(typeof(MappingProfileWeb), typeof(MappingProfileInfrastructure));

            services.AddControllersWithViews();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();
                endpoints.MapRazorPages().RequireAuthorization();
            });

            SeedData.Seed(app, Configuration);
        }

        /// <summary>
        /// Add services to DI.
        /// </summary>
        /// <param name="services">Collection of service</param>
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFormulaOneTeamService, FormulaOneTeamService>();
        }

        /// <summary>
        /// Add repositories to DI.
        /// </summary>
        /// <param name="services">Collection of service</param>
        public void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IFormulaOneTeamRepository, FormulaOneTeamRepository>();
        }
    }
}

using FormulaOne.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FormulaOne.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly string _adminUsername;
        private readonly string _adminEmail;
        private readonly string _adminPassword;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            _adminUsername = config.GetValue<string>("AdminData:Username");
            _adminEmail = config.GetValue<string>("AdminData:E-mail");
            _adminPassword = config.GetValue<string>("AdminData:Password");
        }

        public DbSet<FormulaOneTeam> FormulaOneTeams { get; set; }
    }
}

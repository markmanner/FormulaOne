using FormulaOne.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FormulaOne.Infrastructure.Data
{
    /// <summary>
    /// Seed datas into database
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Insert some test data into the database.
        /// </summary>
        /// <param name="app"></param>
        public static void Seed(IApplicationBuilder app, IConfiguration config)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                string adminUsername = config.GetValue<string>("AdminData:Username");
                string adminNormalizedUsername = config.GetValue<string>("AdminData:NormalizedUsername");
                string adminEmail = config.GetValue<string>("AdminData:E-mail");
                string adminPassword = config.GetValue<string>("AdminData:Password");
                string adminId = Guid.NewGuid().ToString();
                string adminRoleId = Guid.NewGuid().ToString();
                PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();

                ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                #region Seed admin user and role
                context.Roles.Add(new IdentityRole()
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

                context.Users.Add(new IdentityUser()
                {
                    Id = adminId,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    UserName = adminUsername,
                    PasswordHash = hasher.HashPassword(null, adminPassword),
                    NormalizedUserName = adminNormalizedUsername
                });

                context.UserRoles.Add(new IdentityUserRole<string>() 
                {
                    UserId = adminId,
                    RoleId = adminRoleId
                });
                #endregion

                #region Seed formula one teams
                context.FormulaOneTeams.AddRange(new List<FormulaOneTeam>()
                {
                    new FormulaOneTeam(){
                    Name = "Ferrari",
                    NumberOfChampionshipWon = 15,
                    FoundationYear = 1929,
                    IsEntryFeePaid = true,
                    IsDeleted = false
                },
                    new FormulaOneTeam(){
                    Name = "Mercedes AMG",
                    NumberOfChampionshipWon = 5,
                    FoundationYear = 2010,
                    IsEntryFeePaid = true,
                    IsDeleted = false
                },
                    new FormulaOneTeam() {    Name = "Red Bull Racing",
                    NumberOfChampionshipWon = 4,
                    FoundationYear = 2005,
                    IsEntryFeePaid = true,
                    IsDeleted = false
                },
                });
                #endregion

                context.SaveChanges();
            }
        }
    }
}

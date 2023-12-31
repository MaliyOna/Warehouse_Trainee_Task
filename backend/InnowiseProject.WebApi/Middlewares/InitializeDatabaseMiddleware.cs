﻿using InnowiseProject.Database;
using InnowiseProject.Database.Constants;
using InnowiseProject.Database.Models;
using InnowiseProject.WebApi.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InnowiseProject.WebApi.Middlewares
{
    public class InitializeDatabaseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOptions<AdministrationConfiguration> administrationConfiguration;
        private static bool isInitialized = false;

        private static object locker = new();

        public InitializeDatabaseMiddleware(RequestDelegate next, IOptions<AdministrationConfiguration> administrationConfiguration)
        {
            this.next = next;
            this.administrationConfiguration = administrationConfiguration;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if (!isInitialized)
            {
                lock (locker)
                {
                    if (!isInitialized)
                    {
                        InitializeDatabase(serviceProvider);
                        CreateUserRoles(serviceProvider).Wait();
                        CreateDefaultAdmin(serviceProvider).Wait();

                        isInitialized = true;
                    }
                }
            }

            await next.Invoke(context);
        }

        private void InitializeDatabase(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
        }

        private async Task CreateDefaultAdmin(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Worker>>();

            var adminRole = await roleManager.FindByNameAsync(Roles.Admin);

            if (!await dbContext.UserRoles.AnyAsync(x => x.RoleId == adminRole.Id))
            {
                var user = new Worker
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = administrationConfiguration.Value.DefaultUserName,
                    IsSystem = true,
                };

                await userManager.CreateAsync(user);

                await userManager.AddPasswordAsync(user, administrationConfiguration.Value.DefaultPassword);
                await userManager.AddToRoleAsync(user, Roles.Admin);
            }
        }
    }
}

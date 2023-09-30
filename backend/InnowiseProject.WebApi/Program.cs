
using InnowiseProject.Database;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories;
using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Configurations;
using InnowiseProject.WebApi.Factories;
using InnowiseProject.WebApi.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Reflection;

namespace InnowiseProject
{
    public class Program
    {
        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((hostContext, services, configuration) => {
                configuration.MinimumLevel.Information();
                configuration.WriteTo.Console();
                configuration.WriteTo.File("logs/app.txt", rollingInterval: RollingInterval.Day);
            });

            var services = builder.Services;

            services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            builder.Services.Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("Authentication"));
            var authConfig = builder.Configuration.GetSection("Authentication").Get<AuthenticationConfiguration>();

            services.AddIdentity<Worker, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.ClaimsIssuer = authConfig.Issuer;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authConfig.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authConfig.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authConfig.GetSecurityKey(),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddSingleton<JwtFactory>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<InitializeDatabaseMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}
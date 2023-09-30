
using InnowiseProject.Database;
using InnowiseProject.Database.Repositories;
using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            //services.AddIdentity<User, Role>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequiredLength = 1;
            //})
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
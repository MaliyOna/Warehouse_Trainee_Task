using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace InnowiseProject.Application
{
    public static class DependencyRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}

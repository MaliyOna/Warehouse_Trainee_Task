using InnowiseProject.WebApi.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace InnowiseProject.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                logger.LogError(ex, "Not found");

                await context.Response.WriteAsync(ex.Message);
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                logger.LogError(ex, "Bad request");

                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                logger.LogError(ex, "An unhandled exception has occurred while executing the request.");

                await context.Response.WriteAsync("Internal Server Error. Please contact the administrator.");
            }
        }
    }
}

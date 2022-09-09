using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using web_api_net5.Exceptions;

namespace web_api_net5.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;

        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException nfe)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(nfe.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"sth went wrong");
            }
        }
    }
}

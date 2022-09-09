using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using System.Diagnostics;
using System.Threading.Tasks;

namespace web_api_net5.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        Stopwatch _stopwatch;
        ILogger<RequestTimeMiddleware> _logger;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger=logger;
            _stopwatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var timeElapsed = _stopwatch.Elapsed.TotalMilliseconds;
            if(timeElapsed>4000)
            {
                var msg = $"Request [{context.Request.Method} at {context.Request.Path} took {timeElapsed} ms]";

                _logger.LogInformation(msg);
            }
        }
    }
}

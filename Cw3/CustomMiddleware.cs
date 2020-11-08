using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Cw3
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("Custom-Header-2", "Custom-Header-Value-2");
            await _next.Invoke(httpContext);
        }
    }
}
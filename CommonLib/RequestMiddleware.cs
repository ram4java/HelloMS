using Microsoft.AspNetCore.Http;

namespace CommonLib
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string referrer = context.Request.Headers["Referrer"].ToString();
            if (string.IsNullOrEmpty(referrer)) {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                await context.Response.WriteAsync("You are not authorized to call this API Directly");
                return;
            }
            else
            {
                await _next(context);
            }
            

        }

    }
}

namespace Gateway.Middleware
{
    public class GatewayMiddleware
    {
        private readonly RequestDelegate _next;

        public GatewayMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers["Referrer"] = "api-gatewat";
            await _next(context); 

        }
    }
}

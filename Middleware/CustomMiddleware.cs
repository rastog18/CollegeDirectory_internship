namespace CRUDmvc.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Log the request details
            Console.WriteLine($"Request: {httpContext.Request.Method} {httpContext.Request.Path}");

            foreach (var header in httpContext.Request.Headers)
            {
                Console.WriteLine($"Request Header: {header.Key} = {header.Value}");
            }

            await _next(httpContext);
            var response = httpContext.Response.StatusCode;
            if (response != 200)
            {
                httpContext.Response.ContentType = "text/html";
                await httpContext.Response.WriteAsync("<html><body>");
                await httpContext.Response.WriteAsync("<p> Error Ecountered:" + response + "</p>");
                await httpContext.Response.WriteAsync("<a href='https://www.youtube.com/watch?v=xvFZjo5PgG0'>Sorry!, Click Here.</a>");
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}

using System;
// Middleware/AuthorizationHeaderMiddleware.cs
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class AuthorizationHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Session.GetString("AuthToken");

        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers["Authorization"] = $"Bearer {token}";
        }

        await _next(context);
    }
}



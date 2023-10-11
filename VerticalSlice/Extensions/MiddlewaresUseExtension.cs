using Microsoft.AspNetCore.Builder;
using VerticalSlice.Middleware;

namespace VerticalSlice.Extensions;

public static class MiddlewaresUseExtension
{
    /// <summary>
    /// Add all the possible middlewares to the application builder
    /// </summary>
    /// <param name="app"></param>
    public static void AddInfrastructureMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
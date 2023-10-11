using Microsoft.AspNetCore.Builder;
using VerticalSlice.Middleware;

namespace VerticalSlice.Extensions;

public static class MiddlewaresUseExtension
{
    public static void AddInfrastructureMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
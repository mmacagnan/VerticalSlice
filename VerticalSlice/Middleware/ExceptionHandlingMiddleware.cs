using System.Text.Json;
using Microsoft.AspNetCore.Http;
using VerticalSlice.Exceptions;
using VerticalSlice.Models;

namespace VerticalSlice.Middleware;

public class ExceptionHandlingMiddleware
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await handleExceptionAsync(context, e);
        }
    }

    private static async Task handleExceptionAsync(HttpContext context, Exception e)
    {
        var statusCode = getStatusCode(e);

        var resp = new BaseResp<object>
        {
            Status = new Status
            {
                Code = statusCode,
                Message = e.Message,
                Token = ""
            },
            Result = new
            {
                IsSuccess = false,
                Message = e.Message,
                Errors = getErrors(e)
            }
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(resp));

    }
    
    private static int getStatusCode(Exception exception) =>
        exception switch
        {
            HttpStatusCodeException statusCodeException  => statusCodeException.StatusCode,
            ValidationException validationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
    
    private static IReadOnlyDictionary<string, string[]> getErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = null;

        if (exception is ValidationException validationException)
        {
            errors = validationException.ErrorsDictionary;
        }

        return errors;
    }
}
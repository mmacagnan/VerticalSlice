using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalSlice.Models;

namespace VerticalSlice.Extensions;

public static class ControllerBaseExtension
{
    public static OkObjectResult OkUnit(this ControllerBase controller, object value, string token)
    {
        var res = new BaseResp<object>
        {
            Status = new Status
            {
                Code = StatusCodes.Status200OK,
                Token = token
            },
            Result = value
        };
        return controller.Ok(res);
    }

    public static ObjectResult NoContentUnit(this ControllerBase controller, string token)
    {
        return controller.StatusCode(StatusCodes.Status204NoContent,
            new BaseResp(StatusCodes.Status204NoContent, string.Empty, token));
    }

    public static ObjectResult InternalServerError(
        this ControllerBase controller, 
        Exception ex, 
        string message = "",
        bool showStackTrace = true)
    {
        if (ex != null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = ex.Message;
            }
            else if (message != ex.Message)
            {
                message += $" - {ex.Message}";
            }

            if (showStackTrace)
            {
                message += $"- STACKTRACE: {ex.StackTrace}";
            }
        }

        var res = new BaseResp(StatusCodes.Status500InternalServerError, message, null);
        return controller.StatusCode(StatusCodes.Status500InternalServerError, res);
    }

    public static ObjectResult NotFoundUnit(this ControllerBase controller, string token, string message = "")
    {
        return controller.NotFound(new BaseResp(StatusCodes.Status404NotFound, message, token));
    }

    public static ObjectResult StatusCodeUnit(
        this ControllerBase controller, 
        int statusCode, 
        object value,
        Exception ex = null, 
        string message = "")
    {
        if (ex != null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = ex.Message;
            }
            else if (message != ex.Message)
            {
                message += $" - {ex.Message}";
            }
        }

        var res = new BaseResp<object>
        {
            Status = new Status
            {
                Code = statusCode,
                Message = message
            },
            Result = value
        };
        return controller.StatusCode(statusCode, res);
    }
}
using System.Text.Json;

namespace Tatargram.Middleware;

public class ExceptionHandler
{
    private readonly RequestDelegate next;

    public ExceptionHandler(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleException(context, e);
        }
    }

    private Task HandleException(HttpContext context, Exception e)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;

        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            status = (int)StatusCodes.Status500InternalServerError,
            message = e.Message
        }));
    }
}
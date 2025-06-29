using EmailSender.Api.ProblemDetail;
using System.Net;
using System.Text.Json;

namespace EmailSender.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var detail = env.IsDevelopment() ? $"{exception.Message} -> {exception.InnerException} -> {exception.StackTrace}" : exception.Message;
        var problem = ApiError.CreateProblem(context, HttpStatusCode.InternalServerError, "Internal Server Error", detail);

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(problem, options);
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(json);
    }
}

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.ExceptionHandler;

public class ApiExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var details = new ProblemDetails
        {
            Title = "Server Error",
            Status = StatusCodes.Status500InternalServerError,
        };

        // Logar o erro

        httpContext.Response.StatusCode = details.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken: cancellationToken);

        return true;
    }
}

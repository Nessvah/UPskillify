using Microsoft.AspNetCore.Diagnostics;

namespace UPskillify_Forum.Middlewares;

public class CustomErrorHandler(ILogger<CustomErrorHandler> _logger) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        string exceptionMessage = exception.Message;
        
        // Store the exception message in the HttpContext.Items collection
        httpContext.Items["ExceptionMessage"] = exceptionMessage;
        
        // Log the error message and its occurrence time
        _logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}",
            exceptionMessage, DateTime.UtcNow);
        
        // Return false to continue with the default behavior
        // - or - return true to signal that this exception is handled
        return ValueTask.FromResult(false);
    }
    
}
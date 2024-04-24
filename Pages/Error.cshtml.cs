using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UPskillify_Forum.Helpers;

namespace UPskillify_Forum.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    

    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        
        // Retrieve the exception message from the HttpContext.Items collection
        var exceptionMessage = HttpContext.Items["ExceptionMessage"] as string;
        
        var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        bool hasExceptionMessage = !string.IsNullOrWhiteSpace(exceptionMessage);
        bool hasPath = !string.IsNullOrWhiteSpace(exceptionHandlerPathFeature?.Path);
        
        if (hasExceptionMessage || hasPath)
        {
            string errorPath = exceptionHandlerPathFeature.Path;
            ErrorHandlingHelper.HandleErrorOutput(_logger, RequestId, exceptionMessage, errorPath);
        }
    }
    
    public void OnPost()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            
        // Retrieve the exception message from the HttpContext.Items collection
        var exceptionMessage = HttpContext.Items["ExceptionMessage"] as string;
        
        var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        bool hasExceptionMessage = !string.IsNullOrWhiteSpace(exceptionMessage);
        bool hasPath = !string.IsNullOrWhiteSpace(exceptionHandlerPathFeature?.Path);
        
        if (hasExceptionMessage || hasPath)
        {
            string errorPath = exceptionHandlerPathFeature.Path;
            ErrorHandlingHelper.HandleErrorOutput(_logger, RequestId, exceptionMessage, errorPath);
        }
    }
}
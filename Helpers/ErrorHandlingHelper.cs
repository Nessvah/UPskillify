namespace UPskillify_Forum.Helpers;

public class ErrorHandlingHelper
{
    public static void HandleErrorOutput(ILogger logger, string requestId, string exceptionMessage, string path,
        string additionalInfo = "")
    {
        bool isValidMessage = !string.IsNullOrWhiteSpace(exceptionMessage);
        bool isValidPath = !string.IsNullOrWhiteSpace(path);

        if (isValidMessage && isValidPath)
        {
            // both path and exception message are present and contain characters
            exceptionMessage += $" - Path: {path} - Additional Info: {additionalInfo}";
            logger.LogError("Request Id: {requestId} - Exception Message: {exceptionMessage}", 
                requestId, exceptionMessage);
        }
        else if (isValidPath)
        {
            // in this case only path is present
            logger.LogError("Request ID: {requestId} - Path: {path} - Additional Info: {additionalInfo}", 
                requestId, path, additionalInfo
            );
        } else if (isValidMessage)
        {
            // in this case only the message is valid
            logger.LogError("Request ID: {requestId} - Exception message: {exceptionMessage} - Additional Info: {additionalInfo}",
                requestId, exceptionMessage, additionalInfo);
        }
    }
}
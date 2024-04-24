namespace UPskillify_Forum.Helpers;

public class ErrorHandlingHelper
{
    public static void HandleErrorOutput(ILogger logger, string exceptionMessage, string path,
        string additionalInfo = "")
    {
        bool isValidMessage = !string.IsNullOrWhiteSpace(exceptionMessage);
        bool isValidPath = !string.IsNullOrWhiteSpace(path);

        if (isValidMessage && isValidPath)
        {
            // both path and exception message are present and contain characters
            exceptionMessage += $" - Path: {path} - Additional Info: {additionalInfo}";
            logger.LogError("Exception Message: {exceptionMessage}", exceptionMessage);
        }
        else if (isValidPath)
        {
            // in this case only path is present
            logger.LogError("Path: {path} - Additional Info: {additionalInfo}", path, additionalInfo
            );
        } else if (isValidMessage)
        {
            // in this case only the message is valid
            logger.LogError("Exception message: {exceptionMessage} - Additional Info: {additionalInfo}",
                exceptionMessage, additionalInfo);
        }
    }
}
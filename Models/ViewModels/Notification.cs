using UPskillify_Forum.Enums;

namespace UPskillify_Forum.Models.ViewModels;

public class Notification
{
    public string Message { get; set; }
    public NotificationType Type { get; set; }
}
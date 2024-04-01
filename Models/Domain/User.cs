namespace UPskillify_Forum.Models.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? ProfilePictureUrl { get; set; }
    // Navigation property to represent users that this user is following
    public List<UserFollow>? Following { get; set; }
    // Navigation property to represent users that are following this user
    public List<UserFollow>? Followers { get; set; }
}
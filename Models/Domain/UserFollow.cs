namespace UPskillify_Forum.Models.Domain;

public class UserFollow
{
    public Guid UserId { get; set; }
    // Represents the user who is being followed
    public User User { get; set; }

    public Guid FollowerId  { get; set; }
    // Represents the user who is the follower
    public User Follower { get; set; }
}
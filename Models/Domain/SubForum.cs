namespace UPskillify_Forum.Models.Domain;

public class SubForum
{
    // we start by adding the properties according to the srs
    public Guid Id { get; set; }
    public string ForumName { get; set; }
    public string Description { get; set; }
    public List<Post>? Posts { get; set; }
    
}
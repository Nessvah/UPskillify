namespace UPskillify_Forum.Models.Domain;

public class Tag
{
    public Guid Id { get; set; }
    public string TagName { get; set; }
    public List<Post>? Posts { get; set; }
}
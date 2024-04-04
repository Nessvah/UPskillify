namespace UPskillify_Forum.Models.Domain;

public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? FeaturedImgUrl { get; set; }
    public string Content { get; set; }
    public DateTime PublishedDate { get; set; }
    
    // Navigation property to represent the associated comments
    public List<Comment>? Comments { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    
    // Navigation property to represent the associated subforum
    public SubForum SubForum { get; set; }
    public Guid SubForumId { get; set; }
    
    public User Author { get; set; }
    public Guid AuthorId { get; set; }
    public List<PostTag>? PostTags { get; set; }
}
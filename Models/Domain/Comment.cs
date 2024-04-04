namespace UPskillify_Forum.Models.Domain;

// comment has a one to many relationship with User Entity
public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime PublishedDate { get; set; }
    
    // Reference the Id of the User who authored the comment
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    
    public Guid PostId { get; set; }
    public Post Post { get; set; }
}
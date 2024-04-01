namespace UPskillify_Forum.Models.Domain;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public User AuthorId { get; set; }
    public DateTime PublishedDate { get; set; }
    public Post PostId { get; set; }
}
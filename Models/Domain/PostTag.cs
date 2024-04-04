namespace UPskillify_Forum.Models.Domain;

// Post and Tag models have a many to many relationship 
// so we need a join table to bridge this gap and hold all the fks pairs
// Modeling with a join entity:
public class PostTag
{
    public Guid PostId { get; set; }
    public Post Post { get; set; }

    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}
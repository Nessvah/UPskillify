using Microsoft.EntityFrameworkCore;
using UPskillify_Forum.Models.Domain;

namespace UPskillify_Forum.Data;

// This class inherits from another class - DbContext
public class UpskillifyDbContext: DbContext
{
    // we need to create the constructor to pass the options to the base class
    public UpskillifyDbContext(DbContextOptions options) : base(options)
    {
    }

    // The DbSet properties allow us to query and manipulate data for each entity in the db.
    // SubForums will be the name from which EF will create the tables on the db
    // and also, the property which we can query do the CRUD operations
    public DbSet<SubForum> SubForums { get; set; }
    public DbSet<Post> Pots { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<UserFollow> UserFollows { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    
    // when we have many to many relationships EF can't map them directly so
    // we need to do that explicitly
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // we need to tell EF that the primary key of user Follow is a
        // composite key, so it has two properties (UserId and FollowerId)
        modelBuilder.Entity<UserFollow>()
            .HasKey(follow => new { follow.UserId, follow.FollowerId });
        
        
        modelBuilder.Entity<UserFollow>()
            // this will point for a specific User entity
            .HasOne(follow => follow.User)
            // specify that each user can many multiple followers
            .WithMany(user => user.Followers)
            // tells that the FK of UserId in the UserFollow table ref. the PK of User table
            .HasForeignKey(follow => follow.UserId)
            // this will prevent the deletion of a User if there's Userfollows entities depending on that
            // we can set to cascade depending on the requirements
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserFollow>()
            .HasOne(follow => follow.Follower)
            .WithMany(user => user.Following)
            .HasForeignKey(follow => follow.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // now we can do the same for the PostTag entity
        modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });
        
        // configure the relationship between PostTag and Post
        modelBuilder.Entity<PostTag>()
            .HasOne(ptag => ptag.Post)
            .WithMany(post => post.PostTags)
            .HasForeignKey(ptag => ptag.PostId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // now configure the relationship between PostTag and the Tag table
        modelBuilder.Entity<PostTag>()
            .HasOne(ptag => ptag.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(ptag => ptag.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        // explicitly configure relationship between comment entity and post
        // this was probably unnecessary. 
        modelBuilder.Entity<Comment>()
            .HasOne(cmt => cmt.Post)
            .WithMany(post => post.Comments)
            .HasForeignKey(cmt => cmt.PostId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
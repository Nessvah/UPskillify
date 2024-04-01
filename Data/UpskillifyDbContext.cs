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
}
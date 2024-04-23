using Microsoft.EntityFrameworkCore;
using UPskillify_Forum.Data;
using UPskillify_Forum.Models.Domain;

namespace UPskillify_Forum.Repositories;

/* C# 12 introduces primary constructors, a concise syntax to declare constructors whose parameters are
 available anywhere in the body of the type. (ex: upskillifyDbContext).
 One common use for primary constructors is to specify parameters for dependency injection. */

public class SubForumRepository(UpskillifyDbContext upskillifyDbContext) : ICrudRepository<SubForum>
{
    public async Task<IEnumerable<SubForum>> GetAllAsync()
    {
        return await upskillifyDbContext.SubForums.ToListAsync();
    }

    public async Task<SubForum?> GetAsync(Guid id)
    {
        return await upskillifyDbContext.SubForums.FindAsync(id);
    }

    public async Task<SubForum?> AddAsync(SubForum? entity)
    {
        // try saving the new data in the database
        await upskillifyDbContext.SubForums.AddAsync(entity);
        // this will actually save the changes to the database
        await upskillifyDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<SubForum?> UpdateAsync(SubForum? entity)
    {
        var subForum = await upskillifyDbContext.SubForums.FindAsync(entity.Id);

        if (subForum != null)
        {
            subForum.ForumName = entity.ForumName;
            subForum.Description = entity.Description;
        }

        await upskillifyDbContext.SaveChangesAsync();
        return subForum;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingSubForum = await upskillifyDbContext.SubForums.FindAsync(id);

        if (existingSubForum != null)
        {
            upskillifyDbContext.SubForums.Remove(existingSubForum);
            await upskillifyDbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
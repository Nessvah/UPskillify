using Microsoft.AspNetCore.Mvc.RazorPages;
using UPskillify_Forum.Data;
using UPskillify_Forum.Models.Domain;

namespace UPskillify_Forum.Pages.Admin.Forums;

public class Edit : PageModel
{
    private readonly UpskillifyDbContext _upskillifyDbContext;

    public Edit(UpskillifyDbContext upskillifyDbContext)
    {
        _upskillifyDbContext = upskillifyDbContext;

    }

    public SubForum SubForum { get; set; }
    public string ErrorMessage { get; set; }
    
    // the page will get an id parameter
    public void OnGet(Guid id)
    {
        try
        {
            // while trying to find a record, is not guaranteed that it will exist so we have a p
            // potential null reference that we need to be aware of
            var subForum = _upskillifyDbContext.SubForums.Find(id);

            if (subForum != null) SubForum = subForum;
            else
                // if we didnt find any record, show that message
                ErrorMessage = $"Forum with the Id of: {id}, not found.";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
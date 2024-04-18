using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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

    [BindProperty]
    public SubForum SubForum { get; set; }
    public string ErrorMessage { get; set; }
    
    // the page will get an id parameter
    public void OnGet(Guid id)
    {
        try
        {
            // while trying to find a record, is not guaranteed that it will exist, so we have a p
            // potential null reference that we need to be aware of
            var subForum = _upskillifyDbContext.SubForums.Find(id);

            if (subForum != null) SubForum = subForum;
            else
                // if we didn't find any record, show that message
                ErrorMessage = $"Forum with the Id of: {id}, not found.";
        }
        catch (SqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IActionResult OnPost()
    {
        try
        {
            var subForum = _upskillifyDbContext.SubForums.Find(SubForum.Id);

            if (subForum != null)
            {
                subForum.ForumName = SubForum.ForumName;
                subForum.Description = SubForum.Description;
            }

            _upskillifyDbContext.SaveChanges();
            return RedirectToPage("/admin/forums/list");
        }
        catch (SqlException e)
        {
            // since the on post return an IAction result we need to adapt to return any errors
            // the example below is not going to work since is not compatible with the returning type
            //ErrorMessage = $"Sorry, an error occured while trying to update the info. Try again later. {e.Message}";
            //return ErrorMessage;
            
            ErrorMessage = $"Sorry, an error occured while trying to update the info. Try again later. {e.Message}";
            ModelState.AddModelError(string.Empty, ErrorMessage);
            
            // now we return the page
            return Page();
        }
        catch (Exception e)
        {
            ErrorMessage = $"Sorry an error occured: {e.Message}. Try again later.";
            ModelState.AddModelError(string.Empty, ErrorMessage);

            return Page();
        }
    }
}
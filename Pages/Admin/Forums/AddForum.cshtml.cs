using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UPskillify_Forum.Data;
using UPskillify_Forum.Models.Domain;
using UPskillify_Forum.Models.ViewModels;

namespace UPskillify_Forum.Pages.Admin.Forums;

public class AddForum : PageModel
{
    private readonly UpskillifyDbContext _upskillifyDbContext;

    // property binding - much cleaner approach to submit forms and
    // read their values
    [BindProperty]
    public AddSubForum AddSubForumReq { get; set; }
    
    // create construct to inject our db context as a parameter
    public AddForum(UpskillifyDbContext upskillifyDbContext)
    {
        _upskillifyDbContext = upskillifyDbContext;

    }
    public void OnGet()
    {
        
    }
    // this will be the post controller
    //IActionResult is an interface provided by ASP.NET Core MVC that represents the result of an action method.
    public IActionResult OnPost()
    {
        var subForum = new SubForum()
        {
            // this will do the mapping with our table
            ForumName = AddSubForumReq.ForumName,
            Description = AddSubForumReq.Description,
        };

        // before saving the data into the db we need to make sure that the data is valid
        if (!ModelState.IsValid)
        {
            // if the model validation fails, lets return a bad request with validation errors
            return Page();
        }
        try
        {
            // try saving the new data in the database
            _upskillifyDbContext.SubForums.Add(subForum);
            // this will actually save the changes to the database
            _upskillifyDbContext.SaveChanges();
            return StatusCode(201);
        }
        catch (DbUpdateException e)
        {
            // Handle database-specific exceptions
            return BadRequest($"Error saving data: {e.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occured: {e.Message}");
        }

    }
}
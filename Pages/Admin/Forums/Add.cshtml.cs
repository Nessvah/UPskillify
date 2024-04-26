using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UPskillify_Forum.Enums;
using UPskillify_Forum.Models.Domain;
using UPskillify_Forum.Models.ViewModels;
using UPskillify_Forum.Repositories;

namespace UPskillify_Forum.Pages.Admin.Forums;

public class Add : PageModel
{
    private readonly ICrudRepository<SubForum>  _subForumRepository;

    // property binding - much cleaner approach to submit forms and
    // read their values
    [BindProperty]
    public AddSubForum AddSubForumReq { get; set; }
    
    // create construct to inject our db context as a parameter
    public Add(ICrudRepository<SubForum> subForumRepository)
    {
        _subForumRepository = subForumRepository;

    }

    // this will be the post controller
    // IActionResult is an interface provided by ASP.NET Core MVC that represents the result of an action method.
    public async Task<IActionResult> OnPost()
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
            
            await _subForumRepository.AddAsync(subForum);
            
            // if we want to redirect the user to another page the best option is to use tempdata 
            // for its long storage between this request and the next one
           var notification = new Notification
            {
                Message = "Forum created successfully!",
                Type = NotificationType.Success
            };
            
            // TempData internally uses session state to persist data between requests. Session state often
            // requires complex objects to be serializable
            // serializes the Notification object into a JSON string representation, which can then be stored in TempData.
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            return RedirectToPage("/admin/forums/list");
        }
        catch (DbUpdateException e)
        {
            // Handle database-specific exceptions
            ViewData["Notification"] = new Notification
            {
                Message = $"An error occured while trying to save the data: {e.Message}",
                Type = NotificationType.Error
            };

            return Page();
        }
        catch (Exception _)
        {
            ViewData["Notification"] = new Notification
            {
                Message = "An error occured on the server. Try again later or contact the site admin.",
                Type = NotificationType.Error
            };

            return Page();
        }
    }
}
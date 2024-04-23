
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UPskillify_Forum.Data;
using UPskillify_Forum.Models.Domain;
using UPskillify_Forum.Models.ViewModels;
using UPskillify_Forum.Repositories;

namespace UPskillify_Forum.Pages.Admin.Forums;

public class List : PageModel
{
    private readonly ICrudRepository<SubForum> _subForumRepository;

    public List(ICrudRepository<SubForum> subForumRepository)
    {
        _subForumRepository = subForumRepository;

    }

    // List to store all the subforums data from db
    public IEnumerable<SubForum> SubForums { get; set; }
    
    public async Task OnGet()
    {
        try
        {
            
            // pattern matching -> The `is` keyword performs type testing, checking whether an object is compatible with
            // a given type. It returns true if the object can be cast to the specified type, and false otherwise.
            if(TempData["Notification"] is string notification)
            {
                // When retrieving the notification from TempData on the subsequent request, we need to deserialize the
                // JSON string back into a Notification object
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notification);
            }
            
            SubForums = await _subForumRepository.GetAllAsync();
        }
        catch (SqlException ex)
        {
            // string.Empty as the key for a model-level error is a convention that indicates the error is not
            // associated with a specific property but applies to the model as a whole
            ModelState.AddModelError(string.Empty, "An error occurred while fetching data. Please " +
                                                   "try again later.");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try " +
                                                   "again later.");
        }
       
    }
}
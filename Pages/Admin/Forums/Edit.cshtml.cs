using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UPskillify_Forum.Data;
using UPskillify_Forum.Enums;
using UPskillify_Forum.Models.Domain;
using UPskillify_Forum.Models.ViewModels;
using UPskillify_Forum.Repositories;

namespace UPskillify_Forum.Pages.Admin.Forums;

public class Edit : PageModel
{
    private readonly ICrudRepository<SubForum> _subForumRepository;

    public Edit(ICrudRepository<SubForum> subForumRepository)
    {
        _subForumRepository = subForumRepository;

    }

    [BindProperty]
    public SubForum SubForum { get; set; }
    public string ErrorMessage { get; set; }
    
    // the page will get an id parameter
    // TODO: This query might take a long if the db is not up.
    public async Task<IActionResult> OnGet(Guid id)
    {
        try
        {
            // while trying to find a record, is not guaranteed that it will exist, so we have a p
            // potential null reference that we need to be aware of
            var subForum = await _subForumRepository.GetAsync(id);

            if (subForum != null) SubForum = subForum;
            else
            {
                // if we didn't find any record, show that message
                ErrorMessage = $"Forum with the Id of: {id}, not found.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            return Page();
        }
        catch (Exception e)
        {
            ErrorMessage = $"An error occured while trying to get the results. Please try again later.";
            ModelState.AddModelError(string.Empty, ErrorMessage);
            return Page();
        }
    }

    // Razor will know which method to choose between Edit or Delete because with asp-page-handler, he will look
    // for any method prefixed with OnPost and then the name specified on the asp-page-handler.
    public async Task<IActionResult> OnPostEdit()
    {
        try
        {
            var subForum = await _subForumRepository.GetAsync(SubForum.Id);

            if (subForum != null)
            {
                subForum.ForumName = SubForum.ForumName;
                subForum.Description = SubForum.Description;
            }

            await _subForumRepository.UpdateAsync(subForum);

            ViewData["Notification"] = new Notification
            {
                Message = "Forum updated successfully!",
                Type = NotificationType.Success
            };

            return Page();
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

    public async Task<IActionResult> OnPostDelete()
    {
        try
        {
            bool isDeleted = await _subForumRepository.DeleteAsync(SubForum.Id);
         
            if (isDeleted)
            {
                return RedirectToPage("/admin/forums/list");
            }

            ErrorMessage = $"We couldn't find the specified forum to delete.";
            ModelState.AddModelError(string.Empty, ErrorMessage);
            return Page();

        }
        catch (SqlException e)
        {
            ErrorMessage = $"Sorry an error occured while trying to delete the forum. Please try again later.";
            ModelState.AddModelError(string.Empty, ErrorMessage);
            return Page();
        }
        catch (Exception e)
        {
            ErrorMessage = $"An unexpected error occured. Try again later or contact the admin.";
            ModelState.AddModelError(string.Empty, ErrorMessage);
            return Page();
        }
    }
}
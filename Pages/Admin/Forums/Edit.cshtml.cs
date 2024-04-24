using System.Text.Json;
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
                ViewData["Notification"] = new Notification
                {
                    Message = $"Sorry but the id {id} doesn't exist.",
                    Type = NotificationType.Error
                };

                return Page();
            }
            return Page();

        }
        catch (SqlException e)
        {
            ViewData["Notification"] = new Notification
            {
                Message = $"An error occured while trying to get the results. {e.Message}",
                Type = NotificationType.Error
            };
            return Page();
        }
        catch (Exception e)
        {
            ViewData["Notification"] = new Notification
            {
                Message = $"An error occured on the server. {e.Message}",
                Type = NotificationType.Error
            };
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
            ViewData["Notification"] = new Notification
            {
                Message = $"An error occured with the database. {e.Message}",
                Type = NotificationType.Error
            };
            return Page();
        }
        catch (Exception e)
        {
            ViewData["Notification"] = new Notification
            {
                Message = $"An error occured on the server. {e.Message}",
                Type = NotificationType.Error
            };
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
                var notification = new Notification
                {
                    Message = "The forum was deleted successfully!",
                    Type = NotificationType.Success
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/admin/forums/list");
            }

            ViewData["Notification"] = new Notification
            {
                Message = $"The id entered: {SubForum.Id}, does not exists.",
                Type = NotificationType.Error
            };
            return Page();

        }
        catch (SqlException e)
        {
            ViewData["Notification"] = new Notification
            {
                Message = $"An error occured with the database. {e.Message}",
                Type = NotificationType.Error
            };
            return Page();
        }
        catch (Exception e)
        {
            ViewData["Notification"] = new Notification
            {
                Message = $"An error occured on the server. {e.Message}",
                Type = NotificationType.Error
            };
            
            return Page();
        }
    }
}
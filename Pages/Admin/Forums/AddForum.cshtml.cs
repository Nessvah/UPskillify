using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UPskillify_Forum.Models.ViewModels;

namespace UPskillify_Forum.Pages.Admin.Forums;

public class AddForum : PageModel
{
    
    // property binding - much cleaner approach to submit forms and
    // read their values
    [BindProperty]
    public AddSubForum AddSubForumReq { get; set; }
    
    public void OnGet()
    {
        
    }
    // this will be the post controller
    public void OnPost()
    {
        
        
    }
}
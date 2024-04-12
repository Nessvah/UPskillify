
using Microsoft.AspNetCore.Mvc.RazorPages;
using UPskillify_Forum.Data;
using UPskillify_Forum.Models.Domain;

namespace UPskillify_Forum.Pages.Admin.Forums;

public class List : PageModel
{
    private readonly UpskillifyDbContext _upskillifyDbContext;

    public List(UpskillifyDbContext upskillifyDbContext)
    {
        _upskillifyDbContext = upskillifyDbContext;

    }

    // List to store all the subforums data from db
    public List<SubForum> SubForums { get; set; }
    
    public void OnGet()
    {
        SubForums = _upskillifyDbContext.SubForums.ToList();
    }
}
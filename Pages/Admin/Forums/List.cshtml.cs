
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    
    public async Task OnGet()
    {
        try
        {
            SubForums = await _upskillifyDbContext.SubForums.ToListAsync();
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
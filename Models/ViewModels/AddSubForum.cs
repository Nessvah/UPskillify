using System.ComponentModel.DataAnnotations;

namespace UPskillify_Forum.Models.ViewModels;

public class AddSubForum
{
    // we start by adding the properties according to the srs
    public Guid Id { get; set; }
    [Required(ErrorMessage = "The forum name is required")]
    public string ForumName { get; set; }
    [Required(ErrorMessage = "A small description is required.")]
    public string Description { get; set; }
    // public List<Post>? Posts { get; set; }
}
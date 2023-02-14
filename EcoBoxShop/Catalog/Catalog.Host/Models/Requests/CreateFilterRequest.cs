using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class CreateFilterRequest
{
    [Required(ErrorMessage = "Fill in this field")]
    [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
    [Display(Name = "Title")]
    public string Title { get; set; } = null!;
}
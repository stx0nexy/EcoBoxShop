using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class UpdateFilterRequest
{
    [Required(ErrorMessage = "Fill in this field")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Fill in this field")]
    [StringLength(50, ErrorMessage = "Title length can't be more than 50.")]
    public string Title { get; set; } = null!;
}
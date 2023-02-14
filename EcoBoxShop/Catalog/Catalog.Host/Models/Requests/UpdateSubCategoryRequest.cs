using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class UpdateSubCategoryRequest
{
    [Required(ErrorMessage = "Fill in this field")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Fill in this field")]
    [StringLength(50, ErrorMessage = "Title length can't be more than 50.")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Fill in this field")]
    [Display(Name = "Category Id")]
    public int CatalogCategoryId { get; set; }
}
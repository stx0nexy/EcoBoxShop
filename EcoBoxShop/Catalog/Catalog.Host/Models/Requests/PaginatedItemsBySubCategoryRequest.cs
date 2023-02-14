using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class PaginatedItemsBySubCategoryRequest
{
    [Required(ErrorMessage = "Fill in this field")]
    [StringLength(50, ErrorMessage = "SubCategory title length can't be more than 50.")]
    public string SubCategory { get; set; } = null!;

    [Required(ErrorMessage = "Fill in this field")]
    [Display(Name = "Page index")]
    public int PageIndex { get; set; }

    [Required(ErrorMessage = "Fill in this field")]
    [Display(Name = "Page size")]
    public int PageSize { get; set; }
}
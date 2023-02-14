using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class ItemByIdRequest
{
    [Required(ErrorMessage = "Fill in this field")]
    public int Id { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class DeleteRequest
{
    [Required(ErrorMessage = "Fill in this field")]
    public int Id { get; set; }
}
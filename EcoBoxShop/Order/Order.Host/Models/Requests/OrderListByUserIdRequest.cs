using System.ComponentModel.DataAnnotations;

namespace Order.Host.Models.Requests;

public class OrderListByUserIdRequest
{
    [Required(ErrorMessage = "Fill in this field")]
    public string UserId { get; set; } = null!;
}
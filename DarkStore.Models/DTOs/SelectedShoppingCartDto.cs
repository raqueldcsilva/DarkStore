using System.ComponentModel.DataAnnotations;

namespace DarkStore.Models.DTOs;

public class SelectedShoppingCartDto
{
    [Required]
    public int ShoppingCartId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}
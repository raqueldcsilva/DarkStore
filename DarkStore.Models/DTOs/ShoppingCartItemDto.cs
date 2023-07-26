namespace DarkStore.Models.DTOs;

public class ShoppingCartItemDto
{
    public int Id { get; set; }
    public int ShoppingCardId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public string? ProductImageUrl { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}
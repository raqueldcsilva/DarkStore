namespace DarkStore.Api.Entities;

public class ShoppingCartItem
{
    public int Id { get; set; }
    public int ShoppingCardId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public ShoppingCart? ShoppingCart { get; set; }
    public Product? Product { get; set; }
}
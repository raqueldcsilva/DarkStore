using System.ComponentModel.DataAnnotations;
namespace DarkStore.Api.Entities;

public class User
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string UserName { get; set; } = string.Empty;
    public ShoppingCart? ShoppingCart { get; set; }
}
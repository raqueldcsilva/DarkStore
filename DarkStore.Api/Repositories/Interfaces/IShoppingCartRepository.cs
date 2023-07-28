using DarkStore.Api.Entities;
using DarkStore.Models.DTOs;

namespace DarkStore.Api.Repositories.Interfaces;

public interface IShoppingCartRepository
{
    Task<ShoppingCartItem> AddItem(SelectedShoppingCartDto selectedShoppingCartDto);
    Task<ShoppingCartItem> UpdateQuantity(int id, UpdateQuantityShoppingCartDto updateQuantityShoppingCartDto);
    Task<ShoppingCartItem> DeleteItem(int id);
    Task<ShoppingCartItem> GetItem(int id);
    Task<IEnumerable<ShoppingCartItem>> GetItems(int userId);
}
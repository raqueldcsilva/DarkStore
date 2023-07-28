using DarkStore.Models.DTOs;

namespace DarkStore.Web.Services.Interfaces;

public interface IShoppingCartService
{
    Task<List<ShoppingCartItemDto>> GetItems(int userId);
    Task<ShoppingCartItemDto> AddItem(SelectedShoppingCartDto selectedShoppingCartDto);
}
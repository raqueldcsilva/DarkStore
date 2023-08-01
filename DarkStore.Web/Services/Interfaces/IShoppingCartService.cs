using DarkStore.Models.DTOs;

namespace DarkStore.Web.Services.Interfaces;

public interface IShoppingCartService
{
    Task<List<ShoppingCartItemDto>> GetItems(int userId);
    Task<ShoppingCartItemDto> AddItem(SelectedShoppingCartDto selectedShoppingCartDto);
    Task<ShoppingCartItemDto> DeleteItem(int id);
    Task<ShoppingCartItemDto> UpdateShoppingCartQuantity(UpdateQuantityShoppingCartDto updateQuantityShoppingCartDto);

    event Action<int> OnShoppingCartChanged;
    void RaiseEventOnShoppingCartChanged(int totalQuantity);
}
using DarkStore.Models.DTOs;

namespace DarkStore.Web.Services.Interfaces;

public interface IManageShoppingCartItemsLocalStorageService
{
    Task<List<ShoppingCartItemDto>> GetCollection();
    Task SaveCollection(List<ShoppingCartItemDto> shoppingCartItemDto);
    Task RemoveCollection();
}
using Blazored.LocalStorage;
using DarkStore.Models.DTOs;
using DarkStore.Web.Services.Interfaces;

namespace DarkStore.Web.Services;

public class ManageShoppingCartItemsLocalStorageService : IManageShoppingCartItemsLocalStorageService
{
    public const string key = "ShoppingCartItemCollection";
    private readonly ILocalStorageService _localStorageService;
    private readonly IShoppingCartService _shoppingCartService;
        
    public ManageShoppingCartItemsLocalStorageService(ILocalStorageService localStorageService, IShoppingCartService shoppingCartService)
    {
        _localStorageService = localStorageService;
        _shoppingCartService = shoppingCartService;
    }

    public async Task<List<ShoppingCartItemDto>> GetCollection()
    {
        return await this._localStorageService.GetItemAsync<List<ShoppingCartItemDto>>(key) ?? await AddCollection();
    }

    public async Task SaveCollection(List<ShoppingCartItemDto> shoppingCartItemDto)
    {
        await this._localStorageService.SetItemAsync(key, shoppingCartItemDto);
    }

    public async Task RemoveCollection()
    {
        await this._localStorageService.RemoveItemAsync(key);
    }
    
    private async Task<List<ShoppingCartItemDto>> AddCollection()
    {
        var shoppingCartCollection = await this._shoppingCartService.GetItems(LoginUser.UserId);
        if (shoppingCartCollection!= null)
        {
            await this._localStorageService.SetItemAsync(key, shoppingCartCollection);
        }
        return shoppingCartCollection;
    }
}
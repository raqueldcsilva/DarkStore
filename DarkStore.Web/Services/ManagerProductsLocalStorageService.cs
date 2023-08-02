using Blazored.LocalStorage;
using DarkStore.Models.DTOs;
using DarkStore.Web.Services.Interfaces;

namespace DarkStore.Web.Services;

public class ManagerProductsLocalStorageService: IManageProductsLocalStorageService
{
    
    private HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly IProductService _productService;

    public ManagerProductsLocalStorageService(HttpClient httpClient, ILocalStorageService localStorageService, IProductService productService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
        _productService = productService;
    }
    
    private const string key = "ProductCollection";
    
    public async Task<IEnumerable<ProductDto>> GetCollection()
    {
        return await this._localStorageService.GetItemAsync<IEnumerable<ProductDto>>(key) ?? await AddCollection();
    }

    public async Task RemoveCollection()
    {
        await this._localStorageService.RemoveItemAsync(key);
    }

    private async Task<IEnumerable<ProductDto>> AddCollection()
    {
        var productCollection = await this._productService.GetProducts();
        if (productCollection!= null)
        {
            await this._localStorageService.SetItemAsync(key, productCollection);
        }
        return productCollection;
    }
}
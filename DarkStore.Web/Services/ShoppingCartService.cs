using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DarkStore.Models.DTOs;
using DarkStore.Web.Services.Interfaces;

namespace DarkStore.Web.Services;

public class ShoppingCartService : IShoppingCartService
{
    public HttpClient _httpClient;
        public ILogger<ShoppingCartService> _loggger;
        
    public ShoppingCartService(HttpClient httpClient, ILogger<ShoppingCartService> loggger)
    {
        _httpClient = httpClient;
        _loggger = loggger;
    }

    public async Task<List<ShoppingCartItemDto>> GetItems(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ShoppingCart/{userId}/items");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ShoppingCartItemDto>().ToList();
                }

                return await response.Content.ReadFromJsonAsync<List<ShoppingCartItemDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status code: {response.StatusCode} Message: {message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ShoppingCartItemDto> AddItem(SelectedShoppingCartDto selectedShoppingCartDto)
    {
        try
        {
            var response =
                await _httpClient.PostAsJsonAsync<SelectedShoppingCartDto>("api/ShoppingCart",
                    selectedShoppingCartDto);
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(ShoppingCartItemDto);
                }

                return await response.Content.ReadFromJsonAsync<ShoppingCartItemDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"{response.StatusCode} Message - {message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ShoppingCartItemDto> DeleteItem(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/ShoppingCart/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ShoppingCartItemDto>();
            }

            return default(ShoppingCartItemDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<ShoppingCartItemDto> UpdateShoppingCartQuantity(UpdateQuantityShoppingCartDto 
    updateQuantityShoppingCartDto)
    {
        try
        {
            var jsonRequest = JsonSerializer.Serialize(updateQuantityShoppingCartDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response =
                await _httpClient.PatchAsync($"api/ShoppingCart/{updateQuantityShoppingCartDto.ShoppingCartItemId}",
                    content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ShoppingCartItemDto>();
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public event Action<int>? OnShoppingCartChanged;
    public void RaiseEventOnShoppingCartChanged(int totalQuantity)
    {
        if (OnShoppingCartChanged != null)
        {
            OnShoppingCartChanged.Invoke(totalQuantity);
        }
    }
}
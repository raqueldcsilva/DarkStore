using System.Net;
using System.Net.Http.Json;
using DarkStore.Models.DTOs;
using DarkStore.Web.Services.Interfaces;
using Exception = System.Exception;

namespace DarkStore.Web.Services;

public class ProductService : IProductService
{
    public HttpClient _HttpClient;
    public ILogger<ProductService> _loggger;

    public ProductService(HttpClient httpClient, ILogger<ProductService> loggger)
    {
        _HttpClient = httpClient;
        _loggger = loggger; 
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        try
        {
            var products = await _HttpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/products");
            return products;
        }
        catch (Exception e)
        {
            _loggger.LogError("Error to access products: api/products");
            throw;
        }
    }

    public async Task<ProductDto> GetProduct(int id)
    {
        try
        {
            var product = await _HttpClient.GetAsync($"api/products/{id}");
            
            if (product.IsSuccessStatusCode)
            {
                if (product.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(ProductDto);
                }

                return await product.Content.ReadFromJsonAsync<ProductDto>();
            }
            else
            {
                var message = await product.Content.ReadAsStringAsync();
                _loggger.LogError($"Error while fetching the product by ID={id} - {message} ");
                throw new Exception($"Status Code: {product.StatusCode} - {message}");
            }
        }
        catch (Exception e)
        {
            _loggger.LogError("Error to access products: api/product");
            throw;
        }
    }

    public async Task<IEnumerable<CategoryDto>> GetCategories()
    {
        try
        {
            var response = await _HttpClient.GetAsync("api/Products/categories");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CategoryDto>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Status Code: {response.StatusCode} - {message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId)
    {
        try
        {
            var response = await _HttpClient.GetAsync($"api/Products/productsByCategory/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ProductDto>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Status Code: {response.StatusCode} - {message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
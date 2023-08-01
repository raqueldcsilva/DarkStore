using DarkStore.Models.DTOs;

namespace DarkStore.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto> GetProduct(int id);
    Task<IEnumerable<CategoryDto>> GetCategories();
    Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId);
}
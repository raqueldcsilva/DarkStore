using DarkStore.Models.DTOs;

namespace DarkStore.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto> GetProduct(int id);
}
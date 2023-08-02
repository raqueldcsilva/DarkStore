using DarkStore.Models.DTOs;

namespace DarkStore.Web.Services.Interfaces;

public interface IManageProductsLocalStorageService
{
    Task<IEnumerable<ProductDto>> GetCollection();
    Task RemoveCollection();
}
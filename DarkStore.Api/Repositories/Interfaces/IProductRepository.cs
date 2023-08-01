using DarkStore.Api.Entities;

namespace DarkStore.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetItems();
    Task<Product> GetItem(int id);
    Task<IEnumerable<Product>> GetItemsByCategory(int id);
    Task<IEnumerable<Category>> GetCategories();
}
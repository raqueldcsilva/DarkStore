using DarkStore.Api.Context;
using DarkStore.Api.Entities;
using DarkStore.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DarkStore.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) => _context = context;
    
    public async Task<IEnumerable<Product>> GetItems()
    {
        var products = await _context.Products.Include(p => p.Category).ToListAsync();
        
        return products;
    }

    public async Task<Product> GetItem(int id)
    {
        var product = await _context.Products.Include(p => p.Category)
                                             .SingleOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
    {
        var products = await _context.Products.Include(p => p.Category)
                                                    .Where(p => p.CategoryId == id).ToListAsync();
        
        return products;
    }
}
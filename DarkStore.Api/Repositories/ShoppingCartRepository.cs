using DarkStore.Api.Context;
using DarkStore.Api.Entities;
using DarkStore.Api.Repositories.Interfaces;
using DarkStore.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DarkStore.Api.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly AppDbContext _context;
    public ShoppingCartRepository(AppDbContext context) => _context = context;

    private async Task<bool> ShoppingCartAlreadyExists(int shoppingCartId, int productId)
    {
        return await _context.ShoppingCartItems.AnyAsync(sc =>
            sc.ShoppingCardId == shoppingCartId && sc.ProductId == productId);
    }

    public async Task<ShoppingCartItem> AddItem(SelectedShoppingCartDto selectedShoppingCartDto)
    {
        if (await ShoppingCartAlreadyExists(selectedShoppingCartDto.ShoppingCartId, selectedShoppingCartDto.ProductId)
            == false)
        {
            //Check if the item already exists
            var item = await (from product in _context.Products
                where product.Id == selectedShoppingCartDto.ProductId
                select new ShoppingCartItem
                {
                    ShoppingCardId = selectedShoppingCartDto.ShoppingCartId,
                    ProductId = product.Id,
                    Quantity = selectedShoppingCartDto.Quantity
                }).SingleOrDefaultAsync();
            
            //if the item exists, add in shopping cart
            if (item is not null)
            {
                var result = await _context.ShoppingCartItems.AddAsync(item);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
        }

        return null;
    }

    public async Task<ShoppingCartItem> UpdateQuantity(int id, UpdateQuantityShoppingCartDto 
    updateQuantityShoppingCartDto)
    {
        var shoppingCartItem = await _context.ShoppingCartItems.FindAsync(id);
        if (shoppingCartItem is not null)
        {
            shoppingCartItem.Quantity = updateQuantityShoppingCartDto.Quantity;
            await _context.SaveChangesAsync();
            return shoppingCartItem;
        }

        return null;
    }

    public async Task<ShoppingCartItem> DeleteItem(int id)
    {
        var item = await _context.ShoppingCartItems.FindAsync(id);

        if (item is not null)
        {
            _context.ShoppingCartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        return item;
    }

    public async Task<ShoppingCartItem> GetItem(int id)
    {
        return await (from shoppingCart in _context.ShoppingCarts
            join shoppingCartItem in _context.ShoppingCartItems
                on shoppingCart.Id equals shoppingCartItem.ShoppingCardId
            where shoppingCartItem.Id == id
            select new
                ShoppingCartItem
                {
                    Id = shoppingCartItem.Id,
                    ProductId = shoppingCartItem.ProductId,
                    Quantity = shoppingCartItem.Quantity,
                    ShoppingCardId = shoppingCartItem.ShoppingCardId
                }).SingleOrDefaultAsync();
    }


    public async Task<IEnumerable<ShoppingCartItem>> GetItems(int userId)
    {
        return await (from shoppingCart in _context.ShoppingCarts
            join shoppingCartItem in _context.ShoppingCartItems
                on shoppingCart.Id equals shoppingCartItem.ShoppingCardId
            where shoppingCart.UserId == userId
            select new
                ShoppingCartItem
                {
                    Id = shoppingCartItem.Id,
                    ProductId = shoppingCartItem.ProductId,
                    Quantity = shoppingCartItem.Quantity,
                    ShoppingCardId = shoppingCartItem.ShoppingCardId
                }).ToListAsync();
    }
}
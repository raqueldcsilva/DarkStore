using DarkStore.Models.DTOs;
using DarkStore.Api.Entities;

namespace DarkStore.Api.Mappings;

public static class MappingDtos
{
    public static IEnumerable<CategoryDto> ConvertCategoryToDto(this IEnumerable<Category> categories)
    {
        return (from category in categories
            select new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();
    }

    public static IEnumerable<ProductDto> ConvertProductsToDto(this IEnumerable<Product> products)
    {
        return (from product in products
            select new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name
            }).ToList();
    }

    public static ProductDto ConvertProductToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryId = product.Category.Id,
            CategoryName = product.Category.Name
        };
    }

    public static IEnumerable<ShoppingCartItemDto> ConvertShoppingCartItemsToDto(this IEnumerable<ShoppingCartItem>
        shoppingCartItems, IEnumerable<Product> products)
    {
        return (from shoppingCartItem in shoppingCartItems
            join product in products on shoppingCartItem.ProductId
                equals product.Id
            select new ShoppingCartItemDto
            {
                Id = shoppingCartItem.Id,
                ProductId = shoppingCartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageUrl = product.ImageUrl,
                Price = product.Price,
                ShoppingCardId = shoppingCartItem.ShoppingCardId,
                Quantity = shoppingCartItem.Quantity,
                TotalPrice = product.Price * shoppingCartItem.Quantity
            }).ToList();
    }
    
    public static ShoppingCartItemDto ConvertShoppingCartItemToDto(this ShoppingCartItem
        shoppingCartItem, Product product)
    {
        return new ShoppingCartItemDto
            {
                Id = shoppingCartItem.Id,
                ProductId = shoppingCartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageUrl = product.ImageUrl,
                Price = product.Price,
                ShoppingCardId = shoppingCartItem.ShoppingCardId,
                Quantity = shoppingCartItem.Quantity,
                TotalPrice = product.Price * shoppingCartItem.Quantity
            };
    }
    
}
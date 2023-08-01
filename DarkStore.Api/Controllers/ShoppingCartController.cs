using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkStore.Api.Entities;
using DarkStore.Api.Mappings;
using DarkStore.Api.Repositories.Interfaces;
using DarkStore.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DarkStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private ILogger<ShoppingCartController> _logger;

    public ShoppingCartController(IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository,
        ILogger<ShoppingCartController> logger)
    {
        _productRepository = productRepository;
        _shoppingCartRepository = shoppingCartRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("{userId:int}/items")]
    public async Task<ActionResult<IEnumerable<ShoppingCartItemDto>>> GetItems(int userId)
    {
        try
        {
            var shoppingCartItems = await _shoppingCartRepository.GetItems(userId);
            if (shoppingCartItems == null)
            {
                return NoContent();
            }

            var products = await this._productRepository.GetItems();
            if (products == null)
            {
                throw new Exception("Don't exists products...");
            }

            var shoppingItemsDto = shoppingCartItems.ConvertShoppingCartItemsToDto(products);
            return Ok(shoppingItemsDto);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while retrieving items from the shopping cart.");
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<ShoppingCartItemDto>> GetItem(int id)
    {
        try
        {
            var shoppingCartItem = await _shoppingCartRepository.GetItem(id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            var product = await this._productRepository.GetItem(shoppingCartItem.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var shoppingCartItemDto = shoppingCartItem.ConvertShoppingCartItemToDto(product);
            return Ok(shoppingCartItemDto);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while retrieving item ={id} from the shopping cart.");
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCartItemDto>> AddItem(
        [FromBody] SelectedShoppingCartDto selectedShoppingCartDto)
    {
        try
        {
            var newShoppingCartItem = await _shoppingCartRepository.AddItem(selectedShoppingCartDto);
            if (newShoppingCartItem == null)
            {
                return NoContent();
            }

            var product = await _productRepository.GetItem(newShoppingCartItem.ProductId);
            if (product == null)
            {
                throw new Exception($"The product wasn't found (Id: {selectedShoppingCartDto.ProductId})");
            }

            var newShoppingCartItemDto = newShoppingCartItem.ConvertShoppingCartItemToDto(product);
            return CreatedAtAction(nameof(GetItem), new { id = newShoppingCartItemDto.Id }, newShoppingCartItemDto);
        }
        catch (Exception e)
        {
            _logger.LogError("Error creation of the new item in Shopping cart");
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ShoppingCartItemDto>> DeleteShoppingCartItem(int id)
    {
        try
        {
            var shoppingCartItem = await _shoppingCartRepository.DeleteItem(id);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetItem(shoppingCartItem.ProductId);

            if (product is null)
            {
                return NotFound();
            }

            var shoppingCartItemDto = shoppingCartItem.ConvertShoppingCartItemToDto(product);
            return Ok(shoppingCartItemDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error deleting item in Shopping cart");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<ShoppingCartItemDto>> UpdateQuantity(int id,
        UpdateQuantityShoppingCartDto updateQuantityShoppingCartDto)
    {
        try
        {
            var shoppingCartItem = await _shoppingCartRepository.UpdateQuantity(id, updateQuantityShoppingCartDto);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetItem(shoppingCartItem.ProductId);
            var shoppingCartItemDto = shoppingCartItem.ConvertShoppingCartItemToDto(product);
            return Ok(shoppingCartItemDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
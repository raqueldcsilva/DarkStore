using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkStore.Api.Mappings;
using DarkStore.Api.Repositories.Interfaces;
using DarkStore.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DarkStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository) => _productRepository = productRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetItems();
                
                if (products is null)
                {
                    return NotFound();
                }
                else
                {
                    var productsDto = products.ConvertProductsToDto();
                    return Ok(productsDto);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetItem(id);
                
                if (product is null)
                {
                    return NotFound();
                }
                else
                {
                    var productDto = product.ConvertProductToDto();
                    return Ok(productDto);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("productsByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            try
            {
                var products = await _productRepository.GetItemsByCategory(categoryId);
                var productDto = products.ConvertProductsToDto();
                return Ok(productDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

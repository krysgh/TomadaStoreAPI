using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.ProductAPI.Services.Interfaces;

namespace TomadaStore.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDTO>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating a product. " + e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductByIdAsync(ObjectId id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while retriving a product. " + e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductAsync([FromBody] ProductRequestDTO productDto)
        {
            try
            {
                await _productService.CreateProductAsync(productDto);
                return Created();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating a product. " + e.StackTrace);
                return StatusCode(500, "Internal Server Error.");
            }
        }
    }
}

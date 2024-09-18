using api.Data;
using api.DTOs;
using api.Models.PhoneModels;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProdcutsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;
        public ProdcutsController(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllProducts(int currentPage = 1)
        {
            var result = await _productService.GetProduct(pageNumber: currentPage);
           return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetProductById(string id, int currentPage = 1)
        {
            var result = await _productService.GetProduct(id, pageNumber: currentPage);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetProductByVariant(string variant, int currentPage = 1 )
        {
            var result = await _productService.GetProduct(variant: variant, pageNumber: currentPage);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetProductByBrand(string brand, int currentPage)
        {
            var result = await _productService.GetProduct(brand: brand, pageNumber: currentPage);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetProductByModel(string model)
        {
            var result = await _productService.GetProduct(model: model);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<Color>> GetColorsByModelId(string modelId)
        {
            var colors = _context.Products
            .Where(p => p.Variant.ModelID == modelId)
            .Select(p => p.Color)
            .Distinct()
            .ToList();
            return Ok(colors);
        }

        [HttpGet]
        public ActionResult<List<Variant>> GetVariantsByModelId(string modelId)
        {
            var variants = _context.Products
            .Where(p => p.Variant.ModelID == modelId)
            .Select(p => p.Variant)
            .Distinct()
            .ToList();
            return Ok(variants);
        }

        [HttpGet]
        public async Task<ActionResult> GetProductByColorAndVariant(string colorID, string variantID, string modelID)
        {
            var result = await _context.Products
                        .Include(b => b.Variant)
                        .Where(p => p.ColorID == colorID && p.VariantID == variantID && p.Variant.ModelID == modelID)
                        .AsNoTracking()
                        .ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewProduct([FromBody] ProductDTO product)
        {
            var existProduct = await _context.Products.FirstOrDefaultAsync(b => b.ProductID == product.ProductID);
            if(existProduct != null)
            {
                return BadRequest("Product already exist");
            }
            var newProduct = new Product
            {
                ProductID = product.ProductID,
                VariantID = product.VariantID,
                ColorID = product.ColorID,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return Ok(newProduct);
        }


       

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(string id, [FromBody] ProductDTO product)
        {
            var existProduct = await _context.Products.FirstOrDefaultAsync(b => b.ProductID == id);
            if(existProduct == null)
            {
                return BadRequest("Product not found");
            }
            existProduct.VariantID = product.VariantID;
            existProduct.ColorID = product.ColorID;
            existProduct.Price = product.Price;
            existProduct.Stock = product.Stock;
            existProduct.Description = product.Description;
            await _context.SaveChangesAsync();
            return Ok(existProduct);
        }

    }
}

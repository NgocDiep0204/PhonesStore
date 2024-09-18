using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.DTOs;
using api.Data;
using api.Models.PhoneModels;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly ApplicationDbContext _context;
        public ModelsController(IProductService productService, IImageService imageService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel([FromForm] ModelDTO modelDto)
        {
            if (modelDto == null)
            {
                return BadRequest("Model data is null.");
            }

            // Save the image file to Cloudinary
            string imagePath = null;
            if (modelDto.Image != null)
            {
                var uploadResult = await _imageService.AddImageAsync(modelDto.Image);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    imagePath = uploadResult.SecureUrl.AbsoluteUri;
                }
                else
                {
                    return StatusCode((int)uploadResult.StatusCode, "Image upload failed.");
                }
            }

            // Create the new model
            var newModel = new Model
            {
                ModelID = Guid.NewGuid().ToString(),
                ModelName = modelDto.ModelName,
                BrandID = modelDto.BrandID,
                ModelImgUrl = imagePath // Save the image path
            };

            _context.Models.Add(newModel);
            await _context.SaveChangesAsync();

            return Ok(newModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModels(int currentPageNumber = 1)
        {
            var models = await _productService.GetModels(pageNumber:currentPageNumber);
            return Ok(models);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(string id)
        {
            var model = await _productService.GetModels(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetModelByName(string name)
        {
            var model = await _productService.GetModels(name: name);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetModelByBrand(string brand)
        {
            var model = await _productService.GetModels(brand: brand);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}

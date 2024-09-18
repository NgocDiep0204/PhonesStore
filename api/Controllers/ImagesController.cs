using api.Data;
using api.DTOs;
using api.Models.PhoneModels;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        public ImagesController(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string productId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            var result = await _imageService.AddImageAsync(file);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            var newImage = new Image
            {
                ImageID = Guid.NewGuid().ToString(),
                PublicId = result.PublicId,
                ImageUrl = result.SecureUrl.AbsoluteUri,
                IsMain = true,
                ProductID = productId
            };

            _context.Images.Add(newImage);
            await _context.SaveChangesAsync();

            return Ok(newImage);
        }
        [HttpGet]
        public async Task<IActionResult> GetImages(string productId)
        {
            var images = await _context.Images
                .Include(i => i.Product)
                .Where(i => i.ProductID == productId)
                .AsNoTracking()
                .ToListAsync();
            return Ok(images);
        }

        [HttpGet]
        public async Task<IActionResult> GetImageByModelID(string modelId)
        {
            var image = _context.Images
                .Include( b => b.Product)
                .ThenInclude(b =>  b.Variant)
                .ThenInclude(b =>  b.Model)
                .Where(b => b.Product.Variant.Model.ModelID == modelId)
                .AsNoTracking()
                 .ToList();
            return Ok(image);
        }
    }
}

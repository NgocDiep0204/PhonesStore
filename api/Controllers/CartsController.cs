using api.Data;
using api.DTOs;
using api.Services;
using api.Models.PhoneModels;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;
        public CartsController(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartsByUserId(string? userId = null)
        {
            var carts = await _productService.GetCarts(userId: userId);
            return Ok(carts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CartDTO cart)
        {
            var newcart = new Cart
            {
                CartID = Guid.NewGuid().ToString(),
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice
            };
            _context.Carts.Add(newcart);
            await _context.SaveChangesAsync();
            return Ok(cart);
        }
    }
}

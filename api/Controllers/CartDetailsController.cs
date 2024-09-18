using api.Data;
using api.Services;
using api.DTOs;
using api.Models.PhoneModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartDetailsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;
        public CartDetailsController(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCartDetails(string? cartID = null)
        {
            var cartDetails = await _productService.GetCartDetails(cartID: cartID);
            return Ok(cartDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartDetailsByUserID(string? userID = null)
        {
            var cartDetails = await _productService.GetCartDetails(userID: userID);
            return Ok(cartDetails);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDetailDTO cartDetail)
        {
            var newCartDetail = new CartDetail
            {
                CartID = cartDetail.CartID,
                ProductID = cartDetail.ProductID,
                Quantity = cartDetail.Quantity,
                IsSelected = true,
            };
            _context.CartDetails.Add(newCartDetail);
            await _context.SaveChangesAsync();

            var totalPrice = await _productService.CalculateTotalPrice(cartDetail.CartID);
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartID == cartDetail.CartID);
            if (cart != null)
            {
                cart.TotalPrice = totalPrice;
                await _context.SaveChangesAsync();
            }

            return Ok(cartDetail);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCartDetail([FromBody] CartDetailDTO cartDetail)
        {
            var existCartDetail = await _context.CartDetails.FirstOrDefaultAsync(b => b.CartID == cartDetail.CartID && b.ProductID == cartDetail.ProductID);
            if(existCartDetail == null)
            {
                return BadRequest("Cart Detail not found");
            }
            existCartDetail.CartID = cartDetail.CartID;
            existCartDetail.ProductID = cartDetail.ProductID;
            existCartDetail.Quantity = cartDetail.Quantity;
            await _context.SaveChangesAsync();

            var totalPrice = await _productService.CalculateTotalPrice(cartDetail.CartID);
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartID == cartDetail.CartID);
            if (cart != null)
            {
                cart.TotalPrice = totalPrice;
                await _context.SaveChangesAsync();
            }

            return Ok(cartDetail);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCartDetail(string cartID, string productID)
        {
            var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(b => b.CartID == cartID && b.ProductID == productID);
            if(cartDetail == null)
            {
                return BadRequest("Cart Detail not found");
            }
            _context.CartDetails.Remove(cartDetail);
            await _context.SaveChangesAsync();

            var totalPrice = await _productService.CalculateTotalPrice(cartDetail.CartID);
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartID == cartDetail.CartID);
            if (cart != null)
            {
                cart.TotalPrice = totalPrice;
                await _context.SaveChangesAsync();
            }

            return Ok(cartDetail);
        }
    }
}

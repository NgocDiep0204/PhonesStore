using api.DTOs;
using api.Function;
using api.Models.PhoneModels;

namespace api.Services
{
    public interface IProductService
    {
        Task<Object> GetProduct(string? id = null, string? model = null, string? variant = null, string? brand = null, int? pageNumber = null);
        Task<Object> GetModels(string? id = null, string? name = null, string? brand = null, int? pageNumber = null);
        Task<List<CartDetail>> GetCartDetails(string? cartID = null, string? userID = null);
        Task<List<Cart>> GetCarts(string? cartID = null, string? userId = null);
        Task<Product> CreateOrUpdateProduct(string? id ,ProductDTO? product);   
        Task<List<Image>> GetImages(string? productId = null);
        Task<List<Brand>> GetBrands(string? id = null, string? name = null);
        Task<decimal> CalculateTotalPrice(string cartID);
    }
}

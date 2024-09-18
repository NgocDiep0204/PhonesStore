using api.Data;
using api.DTOs;
using api.Function;
using api.Models.PhoneModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System.Drawing.Printing;

namespace api.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Object> GetProduct(string? id = null, string? model = null, string? variant = null, string? brand = null, int? pageNumber = null )
        {
            var query = _context.Products
                                .Include(b => b.Variant)
                                .ThenInclude(v => v.Model)
                                .Include(b => b.Color)
                                .Include(b=> b.Images)
                                .AsQueryable();
            if(!string.IsNullOrEmpty(id))
            {
                query = query.Where(b => b.ProductID == id);
            }
            if(!string.IsNullOrEmpty(variant))
            {
                query = query.Where(b => b.Variant.VariantID == variant);
            }
            if(!string.IsNullOrEmpty(brand))
            {
                query = query.Where(b => b.Variant.Model.Brand.BrandName.ToLower().Contains(brand.ToLower().Trim()));
            }
            if(!string.IsNullOrEmpty(model))
            {
                query = query.Where(b => b.Variant.Model.ModelID == model);
            }
            var result = await query.AsNoTracking().ToListAsync();
            if (!pageNumber.HasValue)
            {
                return new { result };
            }
            else
            {
                var paged = PagedList<Product>.Create(result, pageNumber.Value, 9);
                return new { paged, paged.TotalPages };
            }
           
        }

        public async Task<Object> GetModels(string? id = null, string? name = null, string? brand = null, int? pageNumber = null)
        {
            var query = _context.Models.Include(b => b.Brand).AsQueryable();

            if(!string.IsNullOrEmpty(id))
            {
                query = query.Where(b => b.ModelID == id);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(b => b.ModelName.ToLower().Contains(name.ToLower().Trim()));
            }
            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(b => b.Brand.BrandID == brand);
            }


            var result = await query.AsNoTracking().ToListAsync();
            if (!pageNumber.HasValue)
            {
                return new { result };
            }
            else
            {
                var paged = PagedList<Model>.Create(result, pageNumber.Value, 9);
                return new { paged, paged.TotalPages };
            }
        }

        public async Task<Product> CreateOrUpdateProduct(string? id, ProductDTO? product)
        {
            var existProduct = await _context.Products.FirstOrDefaultAsync(b => b.ProductID == product.ProductID);
            if(string.IsNullOrEmpty(id))
            {
                var newProduct = new Product
                {
                    ProductID = Guid.NewGuid().ToString(),
                    VariantID = product.VariantID,
                    ColorID = product.ColorID,
                    Price = product.Price,
                    Stock = product.Stock,
                    Description = product.Description
                };
                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();
                return newProduct;
            }
            else
            {
                var updateProduct = await _context.Products.FirstOrDefaultAsync(b => b.ProductID == id);
                if(updateProduct == null)
                {
                    return null;
                }
                updateProduct.VariantID = product.VariantID;
                updateProduct.ColorID = product.ColorID;
                updateProduct.Price = product.Price;
                updateProduct.Stock = product.Stock;
                updateProduct.Description = product.Description;
                await _context.SaveChangesAsync();
                return updateProduct;
            }
        }

        public Task<List<CartDetail>> GetCartDetails(string? cartID = null, string? userID = null)
        {
            var query = _context.CartDetails
                                .Include(b => b.Product)
                                .ThenInclude(b => b.Variant)
                                .ThenInclude(b => b.Model)
                                .Include(b => b.Product)
                                .ThenInclude(p => p.Color)
                                .Include(b => b.Cart)
                                .AsQueryable();
            if(!string.IsNullOrEmpty(cartID))
                {
                query = query.Where(b => b.CartID == cartID);
            }
            if(!string.IsNullOrEmpty(userID))
            {
                query = query.Where(b => b.Cart.UserId == userID);
            }
            return query.AsNoTracking()
                     .ToListAsync();
        }

        public Task<List<Cart>> GetCarts(string? cartID = null, string? userId = null)
        {
           var query = _context.Carts
                                .Include(b => b.identityUser)
                                .AsQueryable();
            if(!string.IsNullOrEmpty(cartID))
            {
                query = query.Where(b => b.CartID == cartID);
            }
            if(!string.IsNullOrEmpty(userId))
            {
                query = query.Where(b => b.UserId == userId);
            }
            if(!string.IsNullOrEmpty(cartID) && !string.IsNullOrEmpty(userId))
            {
                query = query.Where(b => b.UserId == userId && b.CartID == cartID);
            }
            return query.AsNoTracking()
                     .ToListAsync();
        }

        public Task<List<Image>> GetImages(string? productId = null)
        {
            var query = _context.Images.AsQueryable();
            if(!string.IsNullOrEmpty(productId)) {
                query = query.Where(b => b.ProductID == productId);
            }
            return query.AsNoTracking()
                     .ToListAsync();
        }

        public Task<List<Brand>> GetBrands(string? id = null, string? name = null)
        {
           var query = _context.Brands.AsQueryable();
            if(!string.IsNullOrEmpty(id))
            {
                query = query.Where(b => b.BrandID == id);
            }
            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(b => EF.Functions.Collate(b.BrandName, "SQL_Latin1_General_CP1_CI_AI").Contains(name));
            }
            return query.AsNoTracking()
                     .ToListAsync();
        }

        public async Task<decimal> CalculateTotalPrice(string cartID)
        {
            var cartDetails = await _context.CartDetails
                                            .Where(cd => cd.CartID == cartID)
                                            .Include(cd => cd.Product)
                                            .ToListAsync();

            return cartDetails.Sum(cd => cd.Product != null && cd.Product.Price.HasValue ? cd.Product.Price.Value * cd.Quantity : 0).Value;
        }

    }
}

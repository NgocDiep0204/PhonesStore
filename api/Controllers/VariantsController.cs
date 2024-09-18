using api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VariantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public VariantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetVariants(string modelID)
        {
            if (string.IsNullOrEmpty(modelID))
            {
                return BadRequest("ModelID cannot be null or empty.");
            }

            var variants = await _context.Variants
                                         .Where(b => b.ModelID == modelID)
                                         .ToListAsync();
            return Ok(variants);
        }
    }
}

using api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ColorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await _context.Colors.ToListAsync();
            return Ok(colors);

        }
        [HttpGet]
        public async Task<IActionResult> GetColor(string id)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(c => c.ColorID == id);
            if (color == null)
            {
                return NotFound();
            }
            return Ok(color);
        }

    }
}

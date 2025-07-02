using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reto3_YD.Models;
using Reto3_YD.Data;
using Microsoft.EntityFrameworkCore;

namespace Reto3_YD.Pages.Api
{
    public class ProductosJsonModel : PageModel
    {
        private readonly AppDbContext _context;

        public ProductosJsonModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetAsync()
        {
            var productos = await _context.Productos.ToListAsync();
            return new JsonResult(productos);
        }
    }
}

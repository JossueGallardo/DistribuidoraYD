using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reto3_YD.Models;
using Reto3_YD.Data;
using Microsoft.EntityFrameworkCore;

namespace Reto3_YD.Pages.Api
{
    public class ClientesJsonModel : PageModel
    {
        private readonly AppDbContext _context;

        public ClientesJsonModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetAsync()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return new JsonResult(clientes);
        }
    }
}

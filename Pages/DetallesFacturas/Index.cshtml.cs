using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reto3_YD.Data;
using Reto3_YD.Models;

namespace Reto3_YD.Pages.DetallesFacturas
{
    public class IndexModel : PageModel
    {
        private readonly Reto3_YD.Data.AppDbContext _context;

        public IndexModel(Reto3_YD.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<DetalleFactura> DetalleFactura { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DetalleFactura = await _context.DetallesFactura
                .Include(d => d.Factura)
                .Include(d => d.Producto).ToListAsync();
        }
    }
}

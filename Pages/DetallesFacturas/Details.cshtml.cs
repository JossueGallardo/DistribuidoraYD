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
    public class DetailsModel : PageModel
    {
        private readonly Reto3_YD.Data.AppDbContext _context;

        public DetailsModel(Reto3_YD.Data.AppDbContext context)
        {
            _context = context;
        }

        public DetalleFactura DetalleFactura { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallefactura = await _context.DetallesFactura.FirstOrDefaultAsync(m => m.Id == id);
            if (detallefactura == null)
            {
                return NotFound();
            }
            else
            {
                DetalleFactura = detallefactura;
            }
            return Page();
        }
    }
}

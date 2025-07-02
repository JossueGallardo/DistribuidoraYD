using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reto3_YD.Data;
using Reto3_YD.Models;

namespace Reto3_YD.Pages.DetallesFacturas
{
    public class EditModel : PageModel
    {
        private readonly Reto3_YD.Data.AppDbContext _context;

        public EditModel(Reto3_YD.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DetalleFactura DetalleFactura { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallefactura =  await _context.DetallesFactura.FirstOrDefaultAsync(m => m.Id == id);
            if (detallefactura == null)
            {
                return NotFound();
            }
            DetalleFactura = detallefactura;
           ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id");
           ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DetalleFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleFacturaExists(DetalleFactura.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DetalleFacturaExists(int id)
        {
            return _context.DetallesFactura.Any(e => e.Id == id);
        }
    }
}

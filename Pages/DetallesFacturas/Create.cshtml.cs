using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reto3_YD.Data;
using Reto3_YD.Models;

namespace Reto3_YD.Pages.DetallesFacturas
{
    public class CreateModel : PageModel
    {
        private readonly Reto3_YD.Data.AppDbContext _context;

        public CreateModel(Reto3_YD.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id");
        ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public DetalleFactura DetalleFactura { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DetallesFactura.Add(DetalleFactura);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

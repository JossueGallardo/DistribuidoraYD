using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reto3_YD.Data;
using Reto3_YD.Models;

namespace Reto3_YD.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly Reto3_YD.Data.AppDbContext _context;

        public IndexModel(Reto3_YD.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Producto> Producto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Producto = await _context.Productos.ToListAsync();
        }
    }
}

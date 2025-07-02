using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reto3_YD.Models;
using Reto3_YD.Data;
using Microsoft.EntityFrameworkCore;

namespace Reto3_YD.Pages.Api
{
    public class EstadisticasJsonModel : PageModel
    {
        private readonly AppDbContext _context;

        public EstadisticasJsonModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetAsync()
        {
            try
            {
                // Obtener estadísticas generales
                var totalProductos = await _context.Productos.CountAsync();
                var totalClientes = await _context.Clientes.CountAsync();
                var totalFacturas = await _context.Facturas.CountAsync();
                
                // Calcular total de ventas
                var totalVentas = await _context.DetallesFactura
                    .SumAsync(df => df.Cantidad * df.PrecioUnitario);
                
                // Productos con stock bajo
                var productosStockBajo = await _context.Productos
                    .Where(p => p.Stock < 10)
                    .CountAsync();

                var estadisticas = new
                {
                    totalProductos = totalProductos,
                    totalClientes = totalClientes,
                    totalFacturas = totalFacturas,
                    totalVentas = totalVentas,
                    productosStockBajo = productosStockBajo,
                    fechaActualizacion = DateTime.Now
                };

                return new JsonResult(estadisticas);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { 
                    error = true, 
                    message = "Error al obtener estadísticas: " + ex.Message 
                });
            }
        }
    }
}

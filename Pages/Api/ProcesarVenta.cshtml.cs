using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reto3_YD.Data;
using Reto3_YD.Models;
using System.Text.Json;

namespace Reto3_YD.Pages.Api
{
    public class ProcesarVentaModel : PageModel
    {
        private readonly AppDbContext _context;

        public ProcesarVentaModel(AppDbContext context)
        {
            _context = context;
        }

        public class VentaRequest
        {
            public int ClienteId { get; set; }
            public List<ItemVenta> Items { get; set; } = new List<ItemVenta>();
        }

        public class ItemVenta
        {
            public int ProductoId { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
        }

        public async Task<JsonResult> OnPostAsync()
        {
            try
            {
                using var reader = new StreamReader(Request.Body);
                var requestBody = await reader.ReadToEndAsync();
                var ventaRequest = JsonSerializer.Deserialize<VentaRequest>(requestBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (ventaRequest == null || !ventaRequest.Items.Any())
                {
                    return new JsonResult(new { success = false, message = "Datos de venta inválidos" });
                }

                // Verificar stock disponible para todos los productos
                foreach (var item in ventaRequest.Items)
                {
                    var producto = await _context.Productos.FindAsync(item.ProductoId);
                    if (producto == null)
                    {
                        return new JsonResult(new { success = false, message = $"Producto con ID {item.ProductoId} no encontrado" });
                    }

                    if (producto.Stock < item.Cantidad)
                    {
                        return new JsonResult(new { success = false, message = $"Stock insuficiente para {producto.Nombre}. Stock disponible: {producto.Stock}" });
                    }
                }

                // Crear la factura
                var factura = new Factura
                {
                    ClienteId = ventaRequest.ClienteId,
                    Fecha = DateTime.Now
                };

                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync();

                // Crear detalles de factura y actualizar stock
                foreach (var item in ventaRequest.Items)
                {
                    var detalleFactura = new DetalleFactura
                    {
                        FacturaId = factura.Id,
                        ProductoId = item.ProductoId,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario
                    };

                    _context.DetallesFactura.Add(detalleFactura);

                    // Actualizar stock
                    var producto = await _context.Productos.FindAsync(item.ProductoId);
                    if (producto != null)
                    {
                        producto.Stock -= item.Cantidad;
                        _context.Update(producto);
                    }
                }

                await _context.SaveChangesAsync();

                return new JsonResult(new { 
                    success = true, 
                    message = "Venta procesada exitosamente",
                    facturaId = factura.Id 
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "Error interno del servidor: " + ex.Message });
            }
        }
    }
}

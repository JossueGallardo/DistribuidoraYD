using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto3_YD.Data;
using Reto3_YD.Models;
using System.ComponentModel.DataAnnotations;

namespace Reto3_YD.Controllers
{
    [ApiController]
    [Route("Api")]
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("ProcesarVenta")]
        public async Task<IActionResult> ProcesarVenta([FromBody] VentaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Datos inválidos" });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                // Verificar que el cliente existe
                var cliente = await _context.Clientes.FindAsync(request.ClienteId);
                if (cliente == null)
                {
                    return BadRequest(new { success = false, message = "Cliente no encontrado" });
                }

                // Verificar stock antes de procesar
                foreach (var item in request.Items)
                {
                    var producto = await _context.Productos.FindAsync(item.ProductoId);
                    if (producto == null)
                    {
                        return BadRequest(new { success = false, message = $"Producto {item.ProductoId} no encontrado" });
                    }

                    if (producto.Stock < item.Cantidad)
                    {
                        return BadRequest(new { success = false, message = $"Stock insuficiente para {producto.Nombre}. Stock disponible: {producto.Stock}" });
                    }
                }

                // Crear la factura
                var factura = new Factura
                {
                    ClienteId = request.ClienteId,
                    Fecha = DateTime.Now
                };

                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync(); // Guardar para obtener el ID de la factura

                // Crear los detalles y actualizar stock
                foreach (var item in request.Items)
                {
                    var producto = await _context.Productos.FindAsync(item.ProductoId);
                    
                    if (producto != null)
                    {
                        // Crear detalle de factura
                        var detalle = new DetalleFactura
                        {
                            FacturaId = factura.Id,
                            ProductoId = item.ProductoId,
                            Cantidad = item.Cantidad,
                            PrecioUnitario = item.PrecioUnitario
                        };

                        _context.DetallesFactura.Add(detalle);

                        // Actualizar stock
                        producto.Stock -= item.Cantidad;
                        _context.Productos.Update(producto);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { 
                    success = true, 
                    message = "Venta procesada correctamente",
                    facturaId = factura.Id
                });

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error interno del servidor: " + ex.Message 
                });
            }
        }

        [HttpGet("Facturas")]
        public async Task<IActionResult> ObtenerFacturas()
        {
            try
            {
                var facturas = await _context.Facturas
                    .Include(f => f.Cliente)
                    .Include(f => f.Detalles)
                        .ThenInclude(d => d.Producto)
                    .OrderByDescending(f => f.Fecha)
                    .ToListAsync();

                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener facturas: " + ex.Message });
            }
        }

        [HttpGet("Facturas/{id}")]
        public async Task<IActionResult> ObtenerFactura(int id)
        {
            try
            {
                var factura = await _context.Facturas
                    .Include(f => f.Cliente)
                    .Include(f => f.Detalles)
                        .ThenInclude(d => d.Producto)
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (factura == null)
                {
                    return NotFound(new { message = "Factura no encontrada" });
                }

                return Ok(factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener factura: " + ex.Message });
            }
        }
    }

    public class VentaRequest
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public List<VentaItem> Items { get; set; } = new List<VentaItem>();
    }

    public class VentaItem
    {
        [Required]
        public int ProductoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioUnitario { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reto3_YD.Data;
using Reto3_YD.Models;
using System.Text.Json;

namespace Reto3_YD.Pages.Api
{
    public class RegistrarClienteModel : PageModel
    {
        private readonly AppDbContext _context;

        public RegistrarClienteModel(AppDbContext context)
        {
            _context = context;
        }

        public class ClienteRequest
        {
            public string Nombre { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        public async Task<JsonResult> OnPostAsync()
        {
            try
            {
                using var reader = new StreamReader(Request.Body);
                var requestBody = await reader.ReadToEndAsync();
                var clienteRequest = JsonSerializer.Deserialize<ClienteRequest>(requestBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (clienteRequest == null || string.IsNullOrEmpty(clienteRequest.Nombre) || string.IsNullOrEmpty(clienteRequest.Email))
                {
                    return new JsonResult(new { success = false, message = "Nombre y email son requeridos" });
                }

                // Verificar si el email ya existe
                var existeCliente = await _context.Clientes.AnyAsync(c => c.Email == clienteRequest.Email);
                if (existeCliente)
                {
                    return new JsonResult(new { success = false, message = "Ya existe un cliente con este email" });
                }

                // Crear nuevo cliente
                var cliente = new Cliente
                {
                    Nombre = clienteRequest.Nombre,
                    Email = clienteRequest.Email
                };

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                return new JsonResult(new { 
                    success = true, 
                    message = "Cliente registrado exitosamente",
                    cliente = new { id = cliente.Id, nombre = cliente.Nombre, email = cliente.Email }
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "Error interno del servidor: " + ex.Message });
            }
        }

        public async Task<JsonResult> OnGetAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return new JsonResult(new { success = false, message = "Email es requerido" });
                }

                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == email);
                if (cliente == null)
                {
                    return new JsonResult(new { success = false, message = "Cliente no encontrado" });
                }

                return new JsonResult(new { 
                    success = true, 
                    cliente = new { id = cliente.Id, nombre = cliente.Nombre, email = cliente.Email }
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "Error interno del servidor: " + ex.Message });
            }
        }
    }
}

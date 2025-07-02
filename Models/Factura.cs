namespace Reto3_YD.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<DetalleFactura> Detalles { get; set; }
    }
}

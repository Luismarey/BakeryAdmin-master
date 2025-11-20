namespace BakeryAdmin.Models
{
    public class Produccion
    {
        public int ProduccionId { get; set; }
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
        public string? NumeroProduccion { get; set; }
        public DateTime FechaProduccion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int? DiasValidos { get; set; }
        public string? NumeroLote { get; set; }
        public int CantidadProducida { get; set; }
        public int CantidadDisponible { get; set; }
    }
}
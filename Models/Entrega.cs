namespace BakeryAdmin.Models
{
    public class Entrega
    {
        public int EntregaId { get; set; }
        public string? NumeroEntrega { get; set; }
        public string? NumeroRuta { get; set; }
        public int OrdenId { get; set; }
        public int? MinutosEstimados { get; set; }
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? EstadoEntrega { get; set; }
    }
}

namespace BakeryAdmin.Models
{
    public class Caja
    {
        public int CajaId { get; set; }
        public string? NumeroCaja { get; set; }
        public string? MetodoPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? TotalTransaccion { get; set; }
        public decimal? TotalDescuento { get; set; }
        public decimal? MontoAPagar { get; set; }
        public decimal? MontoRecibido { get; set; }
        public decimal? Cambio { get; set; }
        public int? OrdenId { get; set; }
    }
}
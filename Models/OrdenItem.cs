namespace BakeryAdmin.Models
{
    public class OrdenItem
    {
        public int OrdenItemId { get; set; }
        public int OrdenId { get; set; }
        public Orden? Orden { get; set; }
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
    }
}

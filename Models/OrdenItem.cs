using System.ComponentModel.DataAnnotations;

namespace BakeryAdmin.Models
{
    public class OrdenItem
    {
        public int OrdenItemId { get; set; }
        public int OrdenId { get; set; }
        public Orden? Orden { get; set; }
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
    }
}

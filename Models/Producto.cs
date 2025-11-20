using System.ComponentModel.DataAnnotations;

namespace BakeryAdmin.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La catergoria es obligatorio")]
        [StringLength(40, ErrorMessage = "No puede superar los 40 caracteres.")]
        public string? Categoria { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(40, ErrorMessage = "No puede superar los 40 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripcion es obligatorio")]
        [StringLength(150, ErrorMessage = "No puede superar los 150 caracteres.")]
        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }
        public string? Fotografia { get; set; }

        [StringLength(20, ErrorMessage = "No puede superar los 20 caracteres.")]
        public string? Unidad { get; set; }

        public bool Disponible { get; set; } = true;

        public ICollection<Produccion> Producciones { get; set; } = new List<Produccion>();
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using BakeryAdmin.Models;
using BakeryAdmin.Services;
using Microsoft.VisualStudio.TextTemplating;
using static BakeryAdmin.Models.Enums;

namespace BakeryAdmin.Models
{
    public class Orden
    {
        public int OrdenId { get; set; }
        public int? PersonaId { get; set; }
        //Relacion con la clase PersonaBase
        [ForeignKey("PersonaId")]
        public PersonaBase? Cliente { get; set; }
        public DateTime FechaOrden { get; set; } = DateTime.UtcNow;
        public EstadoOrden EstadoOrden { get; set; }
        public string? Nota { get; set; }
        public MetodoPago MetodoPago { get; set; }
        //Parte de Services
        public MetodosDePago MetodoDePago { get; set; }
        public DateTime? FechaRecibido { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal GranTotal { get; set; }
        public int? EntregaDireccionId { get; set; }
        public ICollection<OrdenItem> Items { get; set; } = new List<OrdenItem>();
        
    }
}

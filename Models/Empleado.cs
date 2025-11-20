using System.ComponentModel.DataAnnotations;
using static BakeryAdmin.Models.Enums;

namespace BakeryAdmin.Models
{
    public class Empleado : PersonaBase
    {

        [StringLength(40, ErrorMessage = "No puede superar los 40 caracteres.")]
        public string? Profesion { get; set; }

        [StringLength(15, ErrorMessage = "No puede superar los 15 caracteres.")]
        public string? Numero_Licencia { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Solo se permiten letras.")]
        [StringLength(2, ErrorMessage = "Máximo 2 letras.")]
        public string? Categoria_Licencia { get; set; }

        public bool Mobilidad { get; set; }

        [StringLength(10, ErrorMessage = "No puede superar los 10 caracteres.")]
        public string? Turno { get; set; }
        
        public Empleado(string nombres, string apellidos, string numCelular)
            : base(nombres, apellidos, numCelular)
        {
            
        }
    }
}

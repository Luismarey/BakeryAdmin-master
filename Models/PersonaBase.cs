using System.ComponentModel.DataAnnotations;
using static BakeryAdmin.Models.Enums;

namespace BakeryAdmin.Models
{
    public class PersonaBase 
    {
        // 1. ATRIBUTOS
        public int PersonaId { get; set; }

        [Required(ErrorMessage = "El nombre es Obligatorio.")]
        [StringLength(40, ErrorMessage = "No puede superar los 40 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es Obligatorio.")]
        [StringLength(40, ErrorMessage = "No puede superar los 40 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El numero de Celular es Obligatorio.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Debe tener entre 8 y 15 dígitos.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números.")]
        public string NumCelular { get; set; } = string.Empty;

        [StringLength(15, ErrorMessage = "No puede superar los 15 caracteres.")]
        public string? NumCi { get; set; }

        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [EmailAddress]
        public string? Correo_Electronico { get; set; }

        public DateTime? Fecha_Nacimiento { get; set; }

        public TipoPersona TipoPersona { get; set; }

        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public bool Active { get; set; } = true;

        // Finalizacion de 1. Atributos

        // Relación con Dirección 
        public ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();


        // 2. CONSTRUCTOR
        public PersonaBase(string nombres, string apellidos, string numCelular) 
        {
            this.Nombres = nombres;
            this.Apellidos = apellidos;
            this.NumCelular = numCelular;
        }

        // Constructor sin parámetros requerido por Entity Framework Core 
        protected PersonaBase()
        {
            Nombres = string.Empty;
            Apellidos = string.Empty;
            NumCelular = string.Empty;
        }
    }
}


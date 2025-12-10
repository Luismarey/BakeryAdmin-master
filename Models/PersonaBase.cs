using System.ComponentModel.DataAnnotations;
using static BakeryAdmin.Models.Enums;

namespace BakeryAdmin.Models
{
    public class PersonaBase 
    {
        // 1. ATRIBUTOS

        //CLIENTE, EMPRESA
        [Key]
        public int PersonaId { get; set; }

        [Required(ErrorMessage = "El nombre es Obligatorio.")]
        [StringLength(40, ErrorMessage = "No puede superar los 40 caracteres.")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es Obligatorio.")]
        [StringLength(40, ErrorMessage = "No puede superar los 40 caracteres.")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El numero de Celular es Obligatorio.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Debe tener entre 8 y 15 dígitos.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números.")]
        public string NumCelular { get; set; }

        [StringLength(15, ErrorMessage = "No puede superar los 15 caracteres.")]
        public string? NumCi { get; set; }

        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [EmailAddress]
        public string? Correo_Electronico { get; set; }

        public DateTime? Fecha_Nacimiento { get; set; }

        //EMPLEADO

        [StringLength(40, ErrorMessage = "No puede superar los 40 caracteres.")]
        public string? Profesion { get; set; }

        [StringLength(15, ErrorMessage = "No puede superar los 15 caracteres.")]
        public string? Numero_Licencia { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Solo se permiten letras.")]
        [StringLength(2, ErrorMessage = "Máximo 2 letras.")]
        public string? Categoria_Licencia { get; set; }
        public bool? Mobilidad { get; set; }

        [StringLength(10, ErrorMessage = "No puede superar los 10 caracteres.")]
        public string? Turno { get; set; }

        public TipoPersona TipoPersona { get; set; }

        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public bool Active { get; set; } = true;


        // Relación con Dirección - Coleccion solo lectura expuesta
        //private readonly ICollection<Direccion> _direcciones = new List<Direccion>();
        public ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();


        // 2. CONSTRUCTOR
        public PersonaBase(string nombres, string apellidos, string numCelular) 
        {
            this.Nombres = nombres;
            this.Apellidos = apellidos;
            this.NumCelular = numCelular;
        }

        //Constructor sin parámetros requerido por Entity Framework Core 
        public PersonaBase()
        {
            Nombres = string.Empty;
            Apellidos = string.Empty;
            NumCelular = string.Empty;
        }
    }
}


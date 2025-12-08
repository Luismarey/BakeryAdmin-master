namespace BakeryAdmin.Models
{
    public class Direccion
    {
        public int DireccionId { get; set; }
        public int PersonaId { get; set; }
        public PersonaBase Persona { get; set; } = null!;
        public string? Zona { get; set; }
        public string? Calle { get; set; }
        public string? Numero { get; set; }
        public string? NombreEdificio { get; set; }
        public string? Referencia { get; set; }
        public string? Ubicacion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
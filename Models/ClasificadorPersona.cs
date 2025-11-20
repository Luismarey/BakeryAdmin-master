namespace BakeryAdmin.Models
{
    public class ClasificadorPersona
    {
        public int ClasificadorPersonaId { get; set; }
        public string? Nombre { get; set; }
        public string? Acrónimo { get; set; }
        public string? Descripción { get; set; }
        public string? RolSistema { get; set; }
        public decimal? Salario { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Activo { get; set; } = true;
    }
}
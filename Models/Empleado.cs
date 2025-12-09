using System.ComponentModel.DataAnnotations;
using static BakeryAdmin.Models.Enums;

namespace BakeryAdmin.Models
{
    public class Empleado : PersonaBase
    {
        public Empleado(string nombres, string apellidos, string numCelular) : base(nombres, apellidos, numCelular)
        {
            
        }
    }
}

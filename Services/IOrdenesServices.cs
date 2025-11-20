using BakeryAdmin.Models;

namespace BakeryAdmin.Services
{
    // Inyeccion de nuevas Dependecias en Ordenes
    public interface IOrdenesService
    {
        Orden CrearOrden(Orden orden);
    }
}
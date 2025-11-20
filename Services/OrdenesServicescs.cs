using BakeryAdmin.Data;
using BakeryAdmin.Models;
using BakeryAdmin.Services;

namespace BakeryAdmin.Services
{
    // Implementación de la interfaz IOrdenesService
    public class OrdenesService : IOrdenesService
    {
        private readonly AppDbContext _dbContext;
        public OrdenesService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para crear una nueva orden
        public Orden CrearOrden(Orden orden)
        {
            orden.Total = orden.Items.Sum(item => item.PrecioUnitario * item.Cantidad);
            orden.TotalDescuento = 0;

            IFormaDePago procesador;

            switch (orden.MetodoDePago)
            {
                case MetodoDePago.Efectivo:
                    procesador = new PagoEfectivo();
                    break;
               
                case MetodoDePago.Tarjeta:
                    procesador = new PagoTarjeta();
                    break;
                default:
                    throw new NotSupportedException("Forma de pago no soportada.");
            }
            return orden;
        }
    }
}


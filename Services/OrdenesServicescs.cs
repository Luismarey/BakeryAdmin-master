using BakeryAdmin.Data;
using BakeryAdmin.Models;
using BakeryAdmin.Services;
using System;

using static BakeryAdmin.Models.Enums;

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
            orden.Total = orden.Items?.Sum(item => item.PrecioUnitario * item.Cantidad) ?? 0;
            orden.TotalDescuento = 0;

            IFormaDePago procesador;

            switch (orden.MetodoPago)
            {
                case MetodoPago.Efectivo:
                    procesador = new PagoEfectivo();
                    break;
               
                case MetodoPago.Qr:
                    procesador = new PagoQR();
                    break;

                case MetodoPago.PagoTarjeta:
                    procesador = new PagoTarjeta();
                    break;
                default:
                    throw new NotSupportedException("Forma de pago no soportada.");
            }
            orden.GranTotal = procesador.ProcesarPago(orden.Total - orden.TotalDescuento);

            //Logica de persistencia
            _dbContext.Ordenes.Add(orden);
            _dbContext.SaveChanges();

            return orden;
        }
    }
}


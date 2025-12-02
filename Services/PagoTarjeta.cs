using BakeryAdmin.Services;

namespace BakeryAdmin.Services
{

    public class PagoTarjeta : IFormaDePago
    {
        private const decimal PorcentajeComision = 0.03m;
        public decimal ProcesarPago(decimal monto)
        {
            // El monto final incluye la comisión.
            return monto + (monto * PorcentajeComision);
        }
        public string ObtenerDescripcion()
        {
            return $"Tarjeta de Crédito/Débito (Comisión: {PorcentajeComision:P})";
        }
    }
}

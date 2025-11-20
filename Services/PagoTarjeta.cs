using BakeryAdmin.Services;

namespace BakeryAdmin.Services
{
    public class PagoTarjeta : IFormaDePago
    {
        private const decimal TasaComision = 0.03m; 
        public decimal ProcesarPago(decimal monto)
        {
            // El monto final incluye la comisión.
            return monto * (1 + TasaComision);
        }
        public string ObtenerDescripcion()
        {
            return $"Tarjeta (Comisión: {TasaComision:P0})"; // Muestra como porcentaje sin decimales
        }
    }
}

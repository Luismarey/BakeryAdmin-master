using BakeryAdmin.Services;

namespace BakeryAdmin.Services
{
    public class PagoQR : IFormaDePago
    {
        private const decimal CargoFijoTransaccion = 0.05m; 
        public decimal ProcesarPago(decimal monto)
        {
            // El monto final incluye la comisión.
            return monto + CargoFijoTransaccion;
        }
        public string ObtenerDescripcion()
        {
           return $"QR/Banca Móvil (Cargo: {CargoFijoTransaccion:C})"; 
        }
    }
}

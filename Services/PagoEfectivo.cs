using BakeryAdmin.Services;

namespace BakeryAdmin.Services
{ 
    public class PagoEfectivo : IFormaDePago
    {
        public decimal ProcesarPago(decimal monto)
        {
            return monto; 
        }

        public string ObtenerDescripcion()
        {
            return "Efectivo";
        }
    }
}
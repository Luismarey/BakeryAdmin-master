namespace BakeryAdmin.Models
{
    public class Enums
    {
        public enum TipoPersona
        {
            Base = 0,
            Cliente = 1,
            Proveedor = 2,
            Empleado = 3,
            Vendedor = 4
        }
        public enum EstadoOrden
        {
            Proceso = 1,
            Pagado = 2,
            Enviado  = 3,
            Entregado = 4
        }
        public enum MetodoPago
        {
            Efectivo = 1,
            Qr = 2,
            PagoTarjeta = 3
        }
        
    }
}

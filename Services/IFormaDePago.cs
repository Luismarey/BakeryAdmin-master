namespace BakeryAdmin.Services
{
    public interface IFormaDePago
    {
        decimal ProcesarPago(decimal monto);
        string ObtenerDescripcion();
    }
} 
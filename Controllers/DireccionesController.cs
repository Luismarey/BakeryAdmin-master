using BakeryAdmin.Data;
using BakeryAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeryAdmin.Controllers
{
    public class DireccionesController : Controller
    {
        private readonly AppDbContext _db;

        public DireccionesController(AppDbContext context)
        {
            _db = context;
        }

        // GET: Direcciones/Listar/5
        [HttpGet]
        public async Task<IActionResult> Listar(int id) // id = PersonaId
        {
            var direcciones = await _db.Direcciones
                                    .Where(d => d.PersonaId == id)
                                    .OrderBy(d => d.DireccionId)
                                    .ToListAsync();

            return PartialView("_ListaDirecciones", direcciones);
        }

        // GET: Direcciones/Obtener/5
        [HttpGet]
        public async Task<IActionResult> Obtener(int id)
        {
            var direccion = await _db.Direcciones.FindAsync(id);

            if (direccion == null)
            {
                return NotFound();
            }

            return Json(new
            {
                direccionId = direccion.DireccionId,
                zona = direccion.Zona,
                calle = direccion.Calle,
                numero = direccion.Numero,
                nombreEdificio = direccion.NombreEdificio,
                referencia = direccion.Referencia,
                activo = direccion.Activo
            });
        }

        // POST: Direcciones/Save
        [HttpPost]
        public async Task<IActionResult> Save(Direccion model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos." });
            }

            if (model.DireccionId == 0)
            {
                _db.Direcciones.Add(model);
            }

            else
            {
                var existing = await _db.Direcciones.FindAsync(model.DireccionId);
                if (existing == null)
                {
                    return Json(new { success = false, message = "Dirección no encontrada." });
                }

                existing.Zona = model.Zona;
                existing.Calle = model.Calle;
                existing.Numero = model.Numero;
                existing.NombreEdificio = model.NombreEdificio;
                existing.Referencia = model.Referencia;
                existing.Activo = model.Activo;
            }

            await _db.SaveChangesAsync();

            //Retornar tabla actualizada
            var direcciones = _db.Direcciones
                                    .Where(x => x.PersonaId == model.PersonaId)
                                    .AsNoTracking()
                                    .ToList();
                
            return PartialView("~/Views/Personas/_ListaDirecciones.cshtml", direcciones);
        }

        // POST: Direcciones/Eliminar/5
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var direccion = await _db.Direcciones.FindAsync(id);

            if (direccion == null)
            {
                return Json(new { success = false, message = "Dirección no encontrada." });
            }

            _db.Direcciones.Remove(direccion);
            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }

        public IActionResult ObtenerDireccion(int id)
        {
            var item = _db.Direcciones.FirstOrDefault(x => x.DireccionId == id);

            if(item == null)
            {
                return NotFound();
            }


            return Json(new
            {
                item.DireccionId,
                item.PersonaId,
                item.Zona,
                item.Calle,
                item.Numero,
                item.NombreEdificio,
                item.Referencia,
                item.Ubicacion
            });
            
        }
    }
}
using BakeryAdmin.Data;
using BakeryAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BakeryAdmin.Controllers
{
    public class ProduccionesController : Controller
    {
        private readonly AppDbContext _db;

        public ProduccionesController(AppDbContext db)
        { _db = db; }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Producciones.Include(p => p.Producto).AsNoTracking().ToListAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            ViewBag.Productos = _db.Productos.Select(p => new SelectListItem
            {
                Value = p.ProductoId.ToString(),
                Text = p.Nombre
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produccion model)
        {
            if (ModelState.IsValid)
            {
                model.CantidadDisponible = model.CantidadProducida;

                model.DiasValidos = (model.FechaVencimiento - model.FechaProduccion).Days;

                _db.Producciones.Add(model);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "El registro se guardó correctamente.";
                return RedirectToAction("Edit", new { id = model.ProduccionId });
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Producciones.FindAsync(id);
            if (p == null) return NotFound();

            ViewBag.Productos = _db.Productos.Select(p => new SelectListItem
            {
                Value = p.ProductoId.ToString(),
                Text = p.Nombre
            }).ToList();

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produccion model)
        {
            if (id != model.ProduccionId) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            model.CantidadDisponible = model.CantidadProducida;

            model.DiasValidos = (model.FechaVencimiento - model.FechaProduccion).Days;

            _db.Producciones.Update(model);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "El registro se guardó correctamente.";
            return RedirectToAction("Edit", new { id = model.ProduccionId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _db.Producciones.Include(p => p.Producto).FirstOrDefaultAsync(x => x.ProduccionId == id);
            if (p == null) return NotFound();
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Producciones.FindAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _db.Producciones.FindAsync(id);
            if (persona == null)
                return Json(new { success = false, message = "No encontrado" });

            _db.Producciones.Remove(persona);
            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
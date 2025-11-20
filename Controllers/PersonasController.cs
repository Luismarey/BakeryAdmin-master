using BakeryAdmin.Data;
using BakeryAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static BakeryAdmin.Models.Enums;

namespace BakeryAdmin.Controllers
{
    public class PersonasController : Controller
    {
        private readonly AppDbContext _db;

        public PersonasController(AppDbContext db)
        { _db = db; }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Personas.AsNoTracking().ToListAsync();
            return View(items);
        }

        private List<SelectListItem> CargarTipos()
        {
            return Enum.GetValues(typeof(TipoPersona))
                                       .Cast<TipoPersona>()
                                       .Select(tp => new SelectListItem
                                       {
                                           Value = ((int)tp).ToString(),
                                           Text = tp.ToString()
                                       }).ToList();
        }

        public IActionResult Create()
        {
            ViewBag.TiposPersona = CargarTipos();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonaBase model)
        {
            if (ModelState.IsValid)
            {
                _db.Personas.Add(model);
                await _db.SaveChangesAsync();

                TempData["SuccessMessage"] = "El registro se guard� correctamente.";
                return RedirectToAction("Edit", new { id = model.PersonaId });
            }

            ViewBag.TiposPersona = CargarTipos();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Personas.FindAsync(id);
            if (p == null) return NotFound();

            ViewBag.TiposPersona = CargarTipos();

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonaBase model)
        {
            if (id != model.PersonaId) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            _db.Personas.Update(model);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "El registro se guard� correctamente.";
            ViewBag.TiposPersona = CargarTipos();
            return RedirectToAction("Edit", new { id = model.PersonaId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _db.Personas.Include(x => x.Direcciones).AsNoTracking().FirstOrDefaultAsync(x => x.PersonaId == id);
            if (p == null) return NotFound();
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Personas.FindAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _db.Personas.FindAsync(id);
            if (persona == null)
                return Json(new { success = false, message = "No encontrado" });

            _db.Personas.Remove(persona);
            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
using BakeryAdmin.Data;
using BakeryAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeryAdmin.Controllers
{
    public class ProductosController : Controller
    {
        private readonly AppDbContext _db;

        public ProductosController(AppDbContext db)
        { 
            _db = db; 
        }

        [Authorize(Roles = "Administrador,Panadero,Cliente,Vendedor")]
        public async Task<IActionResult> Index()
        {
            var items = await _db.Productos.AsNoTracking().ToListAsync();
            return View(items);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create() => View();

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto model, IFormFile? Fotografia)
        {
            if (ModelState.IsValid)
            {
                if (Fotografia != null && Fotografia.Length > 0)
                {
                    // Generar un nombre unico
                    var fileName = Guid.NewGuid() + Path.GetExtension(Fotografia.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Fotografia.CopyToAsync(stream);
                    }

                    model.Fotografia = fileName;
                }

                _db.Productos.Add(model);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "El registro se guardo correctamente.";
                return RedirectToAction("Edit", new { id = model.ProductoId });
            }
            return View(model);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p == null) 
            {
                return NotFound();
            }

            return View(p);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto model, IFormFile? Fotografia)
        {
            if (id != model.ProductoId) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            if (Fotografia != null && Fotografia.Length > 0)
            {
                // Generar un nombre unico
                var fileName = Guid.NewGuid() + Path.GetExtension(Fotografia.FileName);
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Fotografia.CopyToAsync(stream);
                }

                model.Fotografia = fileName;
            }

            _db.Productos.Update(model);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "El registro se guardo correctamente.";
            return RedirectToAction("Edit", new { id = model.ProductoId });
        }

        [Authorize(Roles = "Administrador,Panadero,Cliente,Vendedor")]
        public async Task<IActionResult> Details(int id)
        {
            var p = await _db.Productos.FirstOrDefaultAsync(x => x.ProductoId == id);
            if (p == null) 
            {
                return NotFound();
            }

            return View(p);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p == null) 
            {
                return NotFound();
            }

            return View(p);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _db.Productos.FindAsync(id);
            if (persona == null)
            {
                return Json(new { success = false, message = "No encontrado" });
            }

            _db.Productos.Remove(persona);
            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult ObtenerProducto(int id)
        {
            var producto = _db.Productos
                .Where(p => p.ProductoId == id)
                .Select(p => new
                {
                    p.ProductoId,
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.Fotografia
                })
                .FirstOrDefault();

            if (producto == null)
            {
                return NotFound();
            }

            return Json(producto);
        }
    }
}
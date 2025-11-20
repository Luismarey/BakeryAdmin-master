using BakeryAdmin.Data;
using BakeryAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static BakeryAdmin.Models.Enums;

namespace BakeryAdmin.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly AppDbContext _db;

        public OrdenesController(AppDbContext db)
        { _db = db; }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Ordenes.Include(x => x.Cliente).AsNoTracking().ToListAsync();
            return View(items);
        }

        private void CargarViewBags()
        {
            ViewBag.Clientes = CargarClientes();
            ViewBag.EstadoOrden = CargarEstadoOrden();
            ViewBag.MetodoPago = CargarMetodoPago();
            ViewBag.Productos = CargarProductos();
        }

        private List<SelectListItem> CargarClientes()
        {
            return _db.Personas.Select(p => new SelectListItem
            {
                Value = p.PersonaId.ToString(),
                Text = p.Nombres + " " + p.Apellidos
            }).ToList();
        }

        private List<SelectListItem> CargarProductos()
        {
            return _db.Productos.Select(p => new SelectListItem
            {
                Value = p.ProductoId.ToString(),
                Text = p.Nombre
            }).ToList();
        }

        private List<SelectListItem> CargarEstadoOrden()
        {
            return Enum.GetValues(typeof(EstadoOrden))
                                       .Cast<EstadoOrden>()
                                       .Select(tp => new SelectListItem
                                       {
                                           Value = ((int)tp).ToString(),
                                           Text = tp.ToString()
                                       }).ToList();
        }

        private List<SelectListItem> CargarMetodoPago()
        {
            return Enum.GetValues(typeof(MetodoPago))
                                       .Cast<MetodoPago>()
                                       .Select(tp => new SelectListItem
                                       {
                                           Value = ((int)tp).ToString(),
                                           Text = tp.ToString()
                                       }).ToList();
        }

        public IActionResult Create()
        {
            CargarViewBags();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orden model)
        {
            if (ModelState.IsValid)
            {
                _db.Ordenes.Add(model);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "El registro se guardó correctamente.";
                return RedirectToAction("Edit", new { id = model.OrdenId });
            }

            CargarViewBags();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Ordenes.Include(p=>p.Items).ThenInclude(p=>p.Producto).AsNoTracking().FirstOrDefaultAsync(x=>x.OrdenId == id);
            if (p == null) return NotFound();

            CargarViewBags();

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Orden model)
        {
            if (id != model.OrdenId) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            model.Total = model.Items?.Sum(i => i.PrecioUnitario * i.Cantidad) ?? 0;
            _db.Ordenes.Update(model);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "El registro se guardó correctamente.";
            CargarViewBags();
            return RedirectToAction("Edit", new { id = model.OrdenId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _db.Ordenes.Include(x => x.Cliente).AsNoTracking().FirstOrDefaultAsync(x => x.OrdenId == id);
            if (p == null) return NotFound();
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Ordenes.FindAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _db.Ordenes.FindAsync(id);
            if (orden == null)
                return Json(new { success = false, message = "No encontrado" });

            _db.Ordenes.Remove(orden);
            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }

        public async Task<IActionResult> DeleteItemConfirmed(int id)
        {
            var orden = await _db.OrdenItems.FindAsync(id);
            if (orden == null)
                return Json(new { success = false, message = "No encontrado" });

            _db.OrdenItems.Remove(orden);
            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }

        public IActionResult EditItem(int id)
        {
            var item = _db.OrdenItems
                .Include(x => x.Producto)
                .FirstOrDefault(x => x.OrdenItemId == id);

            ViewBag.Productos = _db.Productos.OrderBy(x => x.Nombre).ToList();

            return PartialView("_EditItemModal", item);
        }

        [HttpPost]
        public IActionResult UpdateItem(OrdenItem model)
        {
            try
            {
                _db.OrdenItems.Update(model);
                _db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public IActionResult LoadItemsTable(int ordenId)
        {
            var items = _db.OrdenItems
                .Include(x => x.Producto)
                .Where(x => x.OrdenId == ordenId)
                .ToList();

            return PartialView("_ListaProductos", items);
        }


        [HttpPost]
        public IActionResult AgregarProducto(OrdenItem model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos inválidos");

            // Guardar en base de datos
            model.Subtotal = model.PrecioUnitario * model.Cantidad;
            _db.OrdenItems.Add(model);

            var orden = _db.Ordenes.Include(p=>p.Items).AsNoTracking().FirstOrDefault(p=> p.OrdenId == model.OrdenId);
            orden.Total = model.Subtotal + orden?.Items.Sum(p=>p.Subtotal) ?? 0;
            _db.Ordenes.Update(orden);

            _db.SaveChanges();

            // Retornar la tabla actualizada como partial
            var ordenItems = _db.OrdenItems.Include(p => p.Producto).AsNoTracking()
                .Where(x => x.OrdenId == model.OrdenId)
                .ToList();

            return PartialView("_ListaProductos", ordenItems);
        }
    }
}
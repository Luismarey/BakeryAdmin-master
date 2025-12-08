using BakeryAdmin.Models;
using BakeryAdmin.Data;
using BakeryAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static BakeryAdmin.Models.Enums;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BakeryAdmin.Controllers
{
    [Authorize]
    public class OrdenesController : Controller
    {
        private readonly IOrdenesService _ordenesServices;
        private readonly AppDbContext _db;

        public OrdenesController(AppDbContext db, IOrdenesService ordenesService)
        { 
            _db = db;
            _ordenesServices = ordenesService; 
        }

        [Authorize(Roles = "Administrador,Repartidor, Panadero, Vendedor,Cliente")]
        public async Task<IActionResult> Index()
        {
            var items = await _db.Ordenes.Include(x => x.Cliente).Include(p => p.Items).AsNoTracking().ToListAsync();
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

        [Authorize(Roles = "Administrador,Vendedor,Cliente")]
        public IActionResult Create()
        {
            CargarViewBags();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Vendedor,Cliente")]
        public IActionResult Create(Orden model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nuevaOrden = _ordenesServices.CrearOrden(model);
                    TempData["SuccessMessage"] = "La Orden se guardo exitosamente.";
                    return RedirectToAction("Edit", new { id = nuevaOrden.OrdenId });
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            CargarViewBags();

            return View(model);
        }

        [Authorize(Roles = "Administrador,Vendedor,Cliente")]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Ordenes.Include(p=>p.Items).ThenInclude(p=>p.Producto).AsNoTracking().FirstOrDefaultAsync(x=>x.OrdenId == id);
            if (p == null) 
            {
                return NotFound();
            }

            CargarViewBags();

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Vendedor,Cliente")]
        public async Task<IActionResult> Edit(int id, Orden model)
        {
            if (id != model.OrdenId) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            //model.Total = model.Items?.Sum(i => i.PrecioUnitario * i.Cantidad) ?? 0;
            _db.Ordenes.Update(model);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "El registro se guardo correctamente.";
            CargarViewBags();
            return RedirectToAction("Edit", new { id = model.OrdenId });
        }

        [Authorize(Roles = "Administrador,Repartidor,Panadero,Vendedor,Cliente")]
        public async Task<IActionResult> Details(int id)
        {
            var p = await _db.Ordenes.Include(x => x.Cliente).Include(p => p.EntregaDireccionId).Include(p => p.Items).ThenInclude(p => p.Producto).AsNoTracking().FirstOrDefaultAsync(x => x.OrdenId == id);

            if (p == null) 
            {
                return NotFound();
            }

            return View(p);
        }

        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Ordenes.FindAsync(id);

            if (p == null) 
            {
                return NotFound();
            }
            return View(p);
        }

        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _db.Ordenes.FindAsync(id);
            if (orden == null)
            {
                return Json(new { success = false, message = "No encontrado" });
            }

            _db.Ordenes.Remove(orden);
            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }

        public async Task<IActionResult> DeleteItemConfirmed(int id)
        {
            var orden = await _db.OrdenItems.FindAsync(id);
            if (orden == null)
            {
                return Json(new { success = false, message = "No encontrado" });
            }
            _db.OrdenItems.Remove(orden);
            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }
/*
        public IActionResult EditItem(int id)
        {
            var item = _db.OrdenItems
                .Include(x => x.Producto)
                .FirstOrDefault(x => x.OrdenItemId == id);

            ViewBag.Productos = _db.Productos.OrderBy(x => x.Nombre).ToList();

            return PartialView("_EditItemModal", item);
        }
*/
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
                .Include(x => x.Orden)
                .Where(x => x.OrdenId == ordenId)
                .ToList();

            return PartialView("_ListaProductos", items);
        }


        [HttpPost]
        public IActionResult AgregarProducto(OrdenItem model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos invalidos");
            }
            // Guardar en base de datos
            model.Subtotal = model.PrecioUnitario * model.Cantidad;
            //Validaciones para agregar un producto
            if (model.OrdenItemId == 0)
            {
                _db.OrdenItems.Add(model);
            }
            else
            {
                _db.OrdenItems.Update(model);
            }

            _db.SaveChanges();

            //Recalcular total de la orden
            var orden = _db.Ordenes
                .Include(p => p.Items)
                .First(p => p.OrdenId == model.OrdenId);
            
            _db.Ordenes.Update(orden);
            _db.SaveChanges();

            // Retornar tabla actualizada
            var ordenItems = _db.OrdenItems
                .Include(p => p.Producto)
                .Where(x => x.OrdenId == model.OrdenId)
                .AsNoTracking()
                .ToList();
            return PartialView("_ListaProductos", ordenItems);

        }

        public IActionResult ObtenerItem(int id)
        {
            var item = _db.OrdenItems.FirstOrDefault(x => x.OrdenItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return Json(new
            {
                item.OrdenItemId,
                item.ProductoId,
                item.Cantidad,
                item.PrecioUnitario,
                item.Descuento,
                item.Subtotal
            });
        }

        [HttpGet]
        public JsonResult GetDireccionesByClientes(int personaId)
        {
            var direcciones = _db.Direcciones
                .Where(d => d.PersonaId == personaId)
                .Select(d => new
                {
                    d.DireccionId,
                    Descripcion = d.Zona! + " " + d.Calle! + " " + d.Numero!
                })
                .ToList();

            return Json(direcciones);
        }
    }
}

using BakeryAdmin.Data;
using BakeryAdmin.Models;
using BakeryAdmin.ViewModels;
using Microsoft.AspNetCore.Authorization;   
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static BakeryAdmin.Models.Enums;

namespace BakeryAdmin.Controllers
{
    [Authorize]
    public class PersonasController : Controller
    {
        private readonly AppDbContext _db;

        public PersonasController(AppDbContext db)
        { _db = db; }

        [Authorize(Roles = "Administrador,Vendedor,Repartidor")]
        public async Task<IActionResult> Index()
        {
            var items = await _db.Personas.Include(p => p.Direcciones).AsNoTracking().ToListAsync();
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

        [Authorize(Roles = "Administrador,Vendedor")]
        public IActionResult Create()
        {
            ViewBag.TiposPersona = CargarTipos();

            var model = new Empleado("", "", "");

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Create(Empleado model) 
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Por favor corrija los errores en el formulario.";
                ViewBag.TiposPersona = CargarTipos();
                return View(model);
            }

            PersonaBase nuevaPersona;
            
            switch (model.TipoPersona)
            {
                case Enums.TipoPersona.Cliente:
                nuevaPersona = new Cliente(model.Nombres, model.Apellidos, model.NumCelular)
                {
                    // Asignar los campos comunes de PersonaBase
                    NumCi = model.NumCi,
                    Correo_Electronico = model.Correo_Electronico,
                    Fecha_Nacimiento = model.Fecha_Nacimiento,
                    TipoPersona = model.TipoPersona,
                    Active = true,
                };
                break;
                
                case Enums.TipoPersona.Empleado:
                nuevaPersona = new Empleado(model.Nombres, model.Apellidos, model.NumCelular)
                {
                    // Asignar los campos comunes de PersonaBase
                    NumCi = model.NumCi,
                    Correo_Electronico = model.Correo_Electronico,
                    Fecha_Nacimiento = model.Fecha_Nacimiento,
                    TipoPersona = model.TipoPersona,
                    Active = true,
                
                    // Asignar los campos específicos de Empleado
                    Profesion = model.Profesion,
                    Numero_Licencia = model.Numero_Licencia,
                    Categoria_Licencia = model.Categoria_Licencia,
                    Mobilidad = model.Mobilidad,
                    Turno = model.Turno
                };
                break;

                case Enums.TipoPersona.Proveedor:
                nuevaPersona = new Proveedor(model.Nombres, model.Apellidos, model.NumCelular)
                {
                    NumCi = model.NumCi,
                    Correo_Electronico = model.Correo_Electronico,
                    Fecha_Nacimiento = model.Fecha_Nacimiento,
                    TipoPersona = model.TipoPersona,
                    Active = true,
                };
                break;

                case Enums.TipoPersona.Vendedor:
                nuevaPersona = new Vendedor(model.Nombres, model.Apellidos, model.NumCelular)
                {
                    NumCi = model.NumCi,
                    Correo_Electronico = model.Correo_Electronico,
                    Fecha_Nacimiento = model.Fecha_Nacimiento,
                    TipoPersona = model.TipoPersona,
                    Active = true,
                };
                break;

                default:
                ModelState.AddModelError(nameof(model.TipoPersona), "Seleccione un Tipo de Persona válido.");
                ViewBag.TiposPersona = CargarTipos();

                return View(model);
            }

            // Persistencia y Cohesión: Añadir y Guardar el objeto nuevaPersona
            

             _db.Personas.Add(nuevaPersona);
             await _db.SaveChangesAsync(); 

             TempData["SuccessMessage"] = "El registro se guardo correctamente.";
    
             return RedirectToAction("Edit", new { id = nuevaPersona.PersonaId }); 

        }

        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Personas.Include(p => p.Direcciones).AsNoTracking().FirstOrDefaultAsync(p => p.PersonaId == id);
            if (p == null) 
            {
                return NotFound();
            }

            ViewBag.TiposPersona = CargarTipos();

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Edit(int id, PersonaBase model)
        {
            if (id != model.PersonaId) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            _db.Personas.Update(model);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "El registro se guardo correctamente.";
            ViewBag.TiposPersona = CargarTipos();
            return RedirectToAction("Edit", new { id = model.PersonaId });
        }

        [Authorize(Roles = "Administrador,Vendedor, Repartidor")]
        public async Task<IActionResult> Details(int id)
        {
            var p = await _db.Personas.Include(x => x.Direcciones).AsNoTracking().FirstOrDefaultAsync(x => x.PersonaId == id);
            if (p == null) 
            {
                return NotFound();
            }

            return View(p);
        }

        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Personas.FindAsync(id);
            if (p == null) 
            {
                return NotFound();
            }

            return View(p);
        }

        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _db.Personas.FindAsync(id);
            if (persona == null)
            {
                return Json(new { success = false, message = "No encontrado" });
            }
            
            _db.Personas.Remove(persona);
            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
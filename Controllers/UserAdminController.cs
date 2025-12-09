using BakeryAdmin.Models;
using BakeryAdmin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Administrador")]
public class UserAdminController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserAdminController(UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager) {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET: /UserAdmin
    public async Task<IActionResult> Index() {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }

    // GET: /UserAdmin/Create
    public IActionResult Create() {
        return View();
    }

    // POST: /UserAdmin/Create
    [HttpPost]
    public async Task<IActionResult> Create(ApplicationUser model, string password) {
        if (ModelState.IsValid) {
            var result = await _userManager.CreateAsync(model, password);
            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    // GET: /UserAdmin/Edit/5
    public async Task<IActionResult> Edit(string id) {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();
        return View(user);
    }

    // POST: /UserAdmin/Edit
    [HttpPost]
    public async Task<IActionResult> Edit(ApplicationUser model) {
        var user = await _userManager.FindByIdAsync(model.Id);

        if (user == null) return NotFound();

        user.UserName = model.UserName;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        user.NombreCompleto = model.NombreCompleto;

        await _userManager.UpdateAsync(user);

        return RedirectToAction("Index");
    }

    // GET: /UserAdmin/AssignRoles/5
    public async Task<IActionResult> AssignRoles(string id) {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var roles = _roleManager.Roles.ToList();
        var userRoles = await _userManager.GetRolesAsync(user);

        var model = new RolesModels {
            UserId = user.Id,
            Email = user.Email,
            Roles = roles.Select(r => new RolCheckbox {
                NombreRol = r.Name,
                IsSelected = userRoles.Contains(r.Name)
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoles(RolesModels model) {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null) return NotFound();

        var current = await _userManager.GetRolesAsync(user);

        await _userManager.RemoveFromRolesAsync(user, current);

        var selected = model.Roles.Where(x => x.IsSelected).Select(x => x.NombreRol);

        await _userManager.AddToRolesAsync(user, selected);

        return RedirectToAction("Index");
    }

    // POST: Disable / Delete
    public async Task<IActionResult> Disable(string id) {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.MaxValue;

        await _userManager.UpdateAsync(user);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(string id) {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        await _userManager.DeleteAsync(user);

        return RedirectToAction("Index");
    }
}
using BakeryAdmin.Data;
using BakeryAdmin.Models;
using BakeryAdmin.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configuraciones de contraseña
    options.Password.RequireDigit = false;   //No requiere numeros
    options.Password.RequiredLength = 1;    //Minimo 1 caracter
    options.Password.RequireNonAlphanumeric = false;  //No requiere simbolos
    options.Password.RequireUppercase = false;  //No requiere mayusculas
    options.Password.RequireLowercase = false;  //No requiere minusculas
    options.Password.RequiredUniqueChars = 0;  //No requiere caracteres unicos
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; //redirigir al login si no es autenticado
    options.AccessDeniedPath = "/Account/AccessDenegado"; //redirigir si no tiene permiso
});

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
    });
    
builder.Services.AddScoped<IOrdenesService, OrdenesService>();

builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    // Seed roles
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userMgr = services.GetRequiredService<UserManager<ApplicationUser>>();
    string[] roles = new[] { "Administrador", "Cliente", "Panadero", "Repartidor", "Vendedor" };
    foreach (var r in roles)
    {
        if (!await roleMgr.RoleExistsAsync(r)) await roleMgr.CreateAsync(new IdentityRole(r));
    }

    // Seed admin user
    var adminEmail = "admin@bakery.local";
    var admin = await userMgr.FindByEmailAsync(adminEmail);
    if (admin == null)
    {
        admin = new ApplicationUser { UserName = "admin", Email = adminEmail, EmailConfirmed = true, NombreCompleto = "Administrador del Sistema", MustChangePassword = false };
        await userMgr.CreateAsync(admin, "Admin#1234");
        await userMgr.AddToRoleAsync(admin, "Administrador");
    }

    //Cliente
    var clienteEmail = "cliente@bakery.local";
    var cliente = await userMgr.FindByEmailAsync(clienteEmail);
    if(cliente == null)
    {
        cliente = new ApplicationUser { UserName = "cliente", Email = clienteEmail, EmailConfirmed = true, NombreCompleto = "Usuario Cliente", MustChangePassword = false };
        await userMgr.CreateAsync(cliente, "Cliente#123");
        await userMgr.AddToRoleAsync(cliente, "Cliente");
    }

    //Panadero
    var panaderoEmail = "panadero@bakery.local";
    var panadero = await userMgr.FindByEmailAsync(panaderoEmail);
    if (panadero == null)
    {
        panadero = new ApplicationUser { UserName = "panadero", Email = panaderoEmail, EmailConfirmed = true, NombreCompleto = "Usuario Panadero", MustChangePassword = false };
        await userMgr.CreateAsync(panadero, "Panadero#1234");
        await userMgr.AddToRoleAsync(panadero, "Panadero");
    }

    //Repartidor
    var repartidorEmail = "repartidor@bakery.local";
    var repartidor = await userMgr.FindByEmailAsync(repartidorEmail);
    if (repartidor == null)
    {
        repartidor = new ApplicationUser { UserName = "repartidor", Email = repartidorEmail, EmailConfirmed = true, NombreCompleto = "Usuario Repartidor", MustChangePassword = false };
        await userMgr.CreateAsync(repartidor, "Repartidor#123");
        await userMgr.AddToRoleAsync(repartidor, "Repartidor");
    }

    //Vendedor
    var vendedorEmail = "vendedor@bakery.local";
    var vendedor = await userMgr.FindByEmailAsync(vendedorEmail);
    if (vendedor == null)
    {
        vendedor = new ApplicationUser { UserName = "vendedor", Email = vendedorEmail, EmailConfirmed = true, NombreCompleto = "Usuario Vendedor", MustChangePassword = false };
        await userMgr.CreateAsync(vendedor, "Vendedor#123");
        await userMgr.AddToRoleAsync(vendedor, "Vendedor");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); 

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Endpoint simple de salud
app.MapGet("/health", () => Results.Text("OK"));

// Endpoint temporal que lista endpoints registrados (util para diagnosticar rutas)
app.MapGet("/routes", (EndpointDataSource ds) =>
{
    var lines = ds.Endpoints
                  .Select(e => (e.DisplayName ?? e.ToString()))
                  .OrderBy(s => s)
                  .ToArray();
    return Results.Text(string.Join("\n", lines), "text/plain");
});

app.Run();
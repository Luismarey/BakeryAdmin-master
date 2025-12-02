using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BakeryAdmin.Migrations
{
    /// <inheritdoc />
    public partial class Migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caja",
                columns: table => new
                {
                    CajaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCaja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetodoPago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalTransaccion = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    TotalDescuento = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    MontoAPagar = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    MontoRecibido = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    Cambio = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    OrdenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caja", x => x.CajaId);
                });

            migrationBuilder.CreateTable(
                name: "ClasificadorPersonas",
                columns: table => new
                {
                    ClasificadorPersonaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acronimo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RolSistema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salario = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasificadorPersonas", x => x.ClasificadorPersonaId);
                });

            migrationBuilder.CreateTable(
                name: "Entregas",
                columns: table => new
                {
                    EntregaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroRuta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdenId = table.Column<int>(type: "int", nullable: false),
                    MinutosEstimados = table.Column<int>(type: "int", nullable: true),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entregas", x => x.EntregaId);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    NumCelular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NumCi = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Correo_Electronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TipoPersona = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Profesion = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Numero_Licencia = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Categoria_Licencia = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Mobilidad = table.Column<bool>(type: "bit", nullable: true),
                    Turno = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonaId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Fotografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Disponible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    DireccionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Zona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreEdificio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.DireccionId);
                    table.ForeignKey(
                        name: "FK_Direcciones_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    OrdenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: true),
                    FechaOrden = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoOrden = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetodoPago = table.Column<int>(type: "int", nullable: false),
                    MetodoDePago = table.Column<int>(type: "int", nullable: false),
                    FechaRecibido = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    TotalDescuento = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    GranTotal = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    EntregaDireccionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.OrdenId);
                    table.ForeignKey(
                        name: "FK_Ordenes_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId");
                });

            migrationBuilder.CreateTable(
                name: "Producciones",
                columns: table => new
                {
                    ProduccionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    NumeroProduccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaProduccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiasValidos = table.Column<int>(type: "int", nullable: true),
                    NumeroLote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadProducida = table.Column<int>(type: "int", nullable: false),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producciones", x => x.ProduccionId);
                    table.ForeignKey(
                        name: "FK_Producciones_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenItems",
                columns: table => new
                {
                    OrdenItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenItems", x => x.OrdenItemId);
                    table.ForeignKey(
                        name: "FK_OrdenItems_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "OrdenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenItems_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "PersonaId", "Active", "Apellidos", "Correo_Electronico", "Fecha_Nacimiento", "Nombres", "NumCelular", "NumCi", "PasswordHash", "TipoPersona", "Username" },
                values: new object[] { 1, true, "Perez", "juan.perez@email.com", new DateTime(1990, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan", "777123456", "1234567", "hashedpassword", 1, "juanp" });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "PersonaId", "Active", "Apellidos", "Categoria_Licencia", "Correo_Electronico", "Fecha_Nacimiento", "Mobilidad", "Nombres", "NumCelular", "NumCi", "Numero_Licencia", "PasswordHash", "Profesion", "TipoPersona", "Turno", "Username" },
                values: new object[] { 2, true, "Lopez", "C", "maria.lopez@email.com", new DateTime(1985, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Maria", "777987654", "7654321", "B54321", "hashedpassword", "Repostera", 3, "Tarde", "marial" });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "PersonaId", "Active", "Apellidos", "Correo_Electronico", "Fecha_Nacimiento", "Nombres", "NumCelular", "NumCi", "PasswordHash", "TipoPersona", "Username" },
                values: new object[,]
                {
                    { 3, true, "Gonzales", "carlos.gonzales@email.com", new DateTime(1992, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos", "700112233", "4567890", "hashedpassword", 2, "carlosg" },
                    { 4, true, "Torrez", "ana.torrez@email.com", new DateTime(1996, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana", "720334455", "9081726", "hashedpassword", 4, "anat" }
                });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "PersonaId", "Active", "Apellidos", "Categoria_Licencia", "Correo_Electronico", "Fecha_Nacimiento", "Mobilidad", "Nombres", "NumCelular", "NumCi", "Numero_Licencia", "PasswordHash", "Profesion", "TipoPersona", "Turno", "Username" },
                values: new object[] { 5, true, "Rojas", "C", "luis.rojas@email.com", new DateTime(1991, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Luis", "789112233", "3344556", "E33333", "hashedpassword", "Cajero", 3, "Noche", "luisr" });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "PersonaId", "Active", "Apellidos", "Correo_Electronico", "Fecha_Nacimiento", "Nombres", "NumCelular", "NumCi", "PasswordHash", "TipoPersona", "Username" },
                values: new object[] { 6, true, "Vargas", "sofia.vargas@email.com", new DateTime(1994, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sofia", "764556677", "5566778", "hashedpassword", 1, "sofiav" });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "PersonaId", "Active", "Apellidos", "Categoria_Licencia", "Correo_Electronico", "Fecha_Nacimiento", "Mobilidad", "Nombres", "NumCelular", "NumCi", "Numero_Licencia", "PasswordHash", "Profesion", "TipoPersona", "Turno", "Username" },
                values: new object[] { 7, true, "Castro", "C", "miguel.castro@email.com", new DateTime(1988, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Miguel", "701223344", "2233445", "G55555", "hashedpassword", "Chofer", 3, "Ma�ana", "miguelc" });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "PersonaId", "Active", "Apellidos", "Correo_Electronico", "Fecha_Nacimiento", "Nombres", "NumCelular", "NumCi", "PasswordHash", "TipoPersona", "Username" },
                values: new object[,]
                {
                    { 8, true, "Mendoza", "patricia.mendoza@email.com", new DateTime(1993, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patricia", "713445566", "6677889", "hashedpassword", 4, "patriciam" },
                    { 9, true, "Ramirez", "jorge.ramirez@email.com", new DateTime(1997, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jorge", "744223311", "9988776", "hashedpassword", 1, "jorger" }
                });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "PersonaId", "Active", "Apellidos", "Categoria_Licencia", "Correo_Electronico", "Fecha_Nacimiento", "Mobilidad", "Nombres", "NumCelular", "NumCi", "Numero_Licencia", "PasswordHash", "Profesion", "TipoPersona", "Turno", "Username" },
                values: new object[] { 10, true, "Suarez", "B", "elena.suarez@email.com", new DateTime(1990, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Elena", "755443322", "1122334", "J88888", "hashedpassword", "Supervisora", 3, "Noche", "elenas" });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductoId", "Categoria", "Descripcion", "Disponible", "Fotografia", "Nombre", "Precio", "Unidad" },
                values: new object[,]
                {
                    { 1, "Panader�a", "Pan fresco elaborado diariamente", true, "/img/pan-frances.jpg", "Pan Franc�s", 1.50m, "Unidad" },
                    { 2, "Panader�a", "Cl�sica marraqueta crujiente", true, "/img/marraqueta.jpg", "Marraqueta", 1.00m, "Unidad" },
                    { 3, "Pasteler�a", "Torta h�meda con cobertura de chocolate", true, "/img/torta-chocolate.jpg", "Torta de Chocolate", 35.00m, "Unidad" },
                    { 4, "Pasteler�a", "Rollo suave con glaseado dulce", true, "/img/rollo-canela.jpg", "Rollo de Canela", 4.50m, "Unidad" },
                    { 5, "Bebidas", "Caf� reci�n pasado", true, "/img/cafe-americano.jpg", "Caf� Americano", 5.00m, "Vaso" }
                });

            migrationBuilder.InsertData(
                table: "Producciones",
                columns: new[] { "ProduccionId", "CantidadDisponible", "CantidadProducida", "DiasValidos", "FechaProduccion", "FechaVencimiento", "NumeroLote", "NumeroProduccion", "ProductoId" },
                values: new object[,]
                {
                    { 1, 200, 200, 2, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "L-001", "PRD-001", 1 },
                    { 2, 350, 350, 2, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "L-002", "PRD-002", 2 },
                    { 3, 15, 15, 10, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "L-003", "PRD-003", 3 },
                    { 4, 50, 50, 5, new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "L-004", "PRD-004", 4 },
                    { 5, 100, 100, 10, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "L-005", "PRD-005", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_PersonaId",
                table: "Direcciones",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_PersonaId",
                table: "Ordenes",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenItems_OrdenId",
                table: "OrdenItems",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenItems_ProductoId",
                table: "OrdenItems",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Producciones_ProductoId",
                table: "Producciones",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Caja");

            migrationBuilder.DropTable(
                name: "ClasificadorPersonas");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "Entregas");

            migrationBuilder.DropTable(
                name: "OrdenItems");

            migrationBuilder.DropTable(
                name: "Producciones");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}

using BakeryAdmin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static BakeryAdmin.Models.Enums;
using System.Reflection.Emit;

namespace BakeryAdmin.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        {

        }

        public DbSet<PersonaBase> Personas { get; set; }
        public DbSet<ClasificadorPersona> ClasificadorPersonas { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Produccion> Producciones { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenItem> OrdenItems { get; set; }
        public DbSet<Caja> Caja { get; set; }
        public DbSet<Entrega> Entregas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuracion de la herencia para PersonaBase
            builder.Entity<PersonaBase>()
                .HasMany(p => p.Direcciones)
                .WithOne(d => (PersonaBase)d.Persona)
                .HasForeignKey(p => p.PersonaId);

            // Configuracion del Discriminador    
            builder.Entity<PersonaBase>()
                .HasDiscriminator<Enums.TipoPersona>(p => p.TipoPersona)
                .HasValue<PersonaBase>(Enums.TipoPersona.Base)
                .HasValue<Cliente>(Enums.TipoPersona.Cliente)
                .HasValue<Empleado>(Enums.TipoPersona.Empleado)
                .HasValue<Proveedor>(Enums.TipoPersona.Proveedor)
                .HasValue<Vendedor>(Enums.TipoPersona.Vendedor);

            // Configuracion global para propiedades decimal
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // Se establece la precisión. Se pueden ajustar los valores según sea necesario
                   property.SetPrecision(18); 
                   property.SetScale(4);
            }

            // Datos iniciales para Personas
            builder.Entity<Cliente>().HasData
            (
                new Cliente("Juan", "Perez", "777123456")
                {
                    PersonaId = 1,
                    NumCi = "1234567",
                    Correo_Electronico = "juan.perez@email.com",
                    Fecha_Nacimiento = new DateTime(1990, 5, 12),
                    TipoPersona = Enums.TipoPersona.Cliente,
                    Username = "juanp",
                    PasswordHash = "hashedpassword",
                    Active = true
                }
            );

            builder.Entity<Empleado>().HasData
            (
                new Empleado("Maria", "Lopez", "777987654")
                {
                    PersonaId = 2,
                    NumCi = "7654321",
                    Correo_Electronico = "maria.lopez@email.com",
                    Fecha_Nacimiento = new DateTime(1985, 11, 23),
                    TipoPersona = Enums.TipoPersona.Empleado,
                    Profesion = "Repostera",
                    Numero_Licencia = "B54321",
                    Categoria_Licencia = "C",
                    Mobilidad = false,
                    Turno = "Tarde",
                    Username = "marial",
                    PasswordHash = "hashedpassword",
                    Active = true
                }
            );

            builder.Entity<Proveedor>().HasData
            (
                new Proveedor("Carlos", "Gonzales", "700112233")
                {
                    PersonaId = 3,
                    NumCi = "4567890",
                    Correo_Electronico = "carlos.gonzales@email.com",
                    Fecha_Nacimiento = new DateTime(1992, 2, 18),
                    TipoPersona = Enums.TipoPersona.Proveedor,
                    Username = "carlosg",
                    PasswordHash = "hashedpassword",
                    Active = true
                }
            );

            builder.Entity<Vendedor>().HasData
            (
                new Vendedor("Ana", "Torrez", "720334455")
                {
                    PersonaId = 4,
                    NumCi = "9081726",
                    Correo_Electronico = "ana.torrez@email.com",
                    Fecha_Nacimiento = new DateTime(1996, 8, 9),
                    TipoPersona = Enums.TipoPersona.Vendedor,
                    Username = "anat",
                    PasswordHash = "hashedpassword",
                    Active = true
                }
            );

            builder.Entity<Empleado>().HasData
                (
                    new Empleado("Luis", "Rojas", "789112233")
                    {
                        PersonaId = 5,
                        NumCi = "3344556",
                        Correo_Electronico = "luis.rojas@email.com",
                        Fecha_Nacimiento = new DateTime(1991, 7, 28),
                        TipoPersona = Enums.TipoPersona.Empleado,
                        Profesion = "Cajero",
                        Numero_Licencia = "E33333",
                        Categoria_Licencia = "C",
                        Mobilidad = false,
                        Turno = "Noche",
                        Username = "luisr",
                        PasswordHash = "hashedpassword",
                        Active = true
                    }
                );

            builder.Entity<Cliente>().HasData
                (
                    new Cliente("Sofia", "Vargas", "764556677")
                    {
                        PersonaId = 6,
                        NumCi = "5566778",
                        Correo_Electronico = "sofia.vargas@email.com",
                        Fecha_Nacimiento = new DateTime(1994, 4, 14),
                        TipoPersona = Enums.TipoPersona.Cliente,
                        Username = "sofiav",
                        PasswordHash = "hashedpassword",
                        Active = true
                    }
                );

            builder.Entity<Empleado>().HasData
                (
                    new Empleado("Miguel", "Castro", "701223344")
                    {
                        PersonaId = 7,
                        NumCi = "2233445",
                        Correo_Electronico = "miguel.castro@email.com",
                        Fecha_Nacimiento = new DateTime(1988, 12, 1),
                        TipoPersona = Enums.TipoPersona.Empleado,
                        Profesion = "Chofer",
                        Numero_Licencia = "G55555",
                        Categoria_Licencia = "C",
                        Mobilidad = true,
                        Turno = "Ma�ana",
                        Username = "miguelc",
                        PasswordHash = "hashedpassword",
                        Active = true
                    }
                );

            builder.Entity<Vendedor>().HasData
                (
                    new Vendedor("Patricia", "Mendoza", "713445566")
                    {
                        PersonaId = 8,
                        NumCi = "6677889",
                        Correo_Electronico = "patricia.mendoza@email.com",
                        Fecha_Nacimiento = new DateTime(1993, 9, 3),
                        TipoPersona = Enums.TipoPersona.Vendedor,
                        Username = "patriciam",
                        PasswordHash = "hashedpassword",
                        Active = true
                    }
                );

            builder.Entity<Cliente>().HasData
                (
                    new Cliente("Jorge", "Ramirez", "744223311")
                    {
                        PersonaId = 9,
                        NumCi = "9988776",
                        Correo_Electronico = "jorge.ramirez@email.com",
                        Fecha_Nacimiento = new DateTime(1997, 6, 21),
                        TipoPersona = Enums.TipoPersona.Cliente,
                        Username = "jorger",
                        PasswordHash = "hashedpassword",
                        Active = true
                    }
                ); 
            
            builder.Entity<Empleado>().HasData
                (
                    new Empleado("Elena", "Suarez", "755443322")
                    {
                        PersonaId = 10,
                        NumCi = "1122334",
                        Correo_Electronico = "elena.suarez@email.com",
                        Fecha_Nacimiento = new DateTime(1990, 10, 30),
                        TipoPersona = Enums.TipoPersona.Empleado,
                        Profesion = "Supervisora",
                        Numero_Licencia = "J88888",
                        Categoria_Licencia = "B",
                        Mobilidad = true,
                        Turno = "Noche",
                        Username = "elenas",
                        PasswordHash = "hashedpassword",
                        Active = true
                    }

                );
            // Datos iniciales para Direcciones
            builder.Entity<Direccion>().HasData
            (
                new Direccion
                {
                    DireccionId = 1,
                    PersonaId = 1,
                    Zona = "Centro",
                    Calle = "Chacra",
                    Numero = "123",
                    NombreEdificio = "Edificio Sol",
                    Referencia = "Cerca del parque",
                    Ubicacion = ""
                },

                new Direccion
                {
                    DireccionId = 2,
                    PersonaId = 2,
                    Zona = "Norte",
                    Calle = "Las Flores",
                    Numero = "456",
                    NombreEdificio = "Residencial Luna",
                    Referencia = "Frente a la plaza",
                    Ubicacion = ""
                },

                new Direccion
                {
                    DireccionId = 3,
                    PersonaId = 3,
                    Zona = "Sur",
                    Calle = "Av. Siempre Viva",
                    Numero = "789",
                    NombreEdificio = "Condominio Estrella",
                    Referencia = "Al lado del supermercado",
                    Ubicacion = ""
                },

                new Direccion
                {
                    DireccionId = 4,
                    PersonaId = 4,
                    Zona = "Este",
                    Calle = "Los Pinos",
                    Numero = "321",
                    NombreEdificio = "Torre Cielo",
                    Referencia = "Detras de la iglesia",
                    Ubicacion = ""
                },

                new Direccion
                {
                    DireccionId = 5,
                    PersonaId = 5,
                    Zona = "Oeste",
                    Calle = "El Sol",
                    Numero = "654",
                    NombreEdificio = "Villa Primavera",
                    Referencia = "Cerca de la escuela",
                    Ubicacion = ""
                },

                new Direccion
                {
                    DireccionId = 6,
                    PersonaId = 6,
                    Zona = "Centro",
                    Calle = "Santa Cruz",
                    Numero = "987",
                    NombreEdificio = "Residencial Arcoiris",
                    Referencia = "Frente al hospital",
                    Ubicacion = ""
                },

                new Direccion
                {
                    DireccionId = 7,
                    PersonaId = 7,
                    Zona = "Norte",
                    Calle = "Los Olivos",
                    Numero = "159",
                    NombreEdificio = "Edificio Horizonte",
                    Referencia = "Al lado del banco",
                    Ubicacion = ""
                },

                new Direccion
                {
                    DireccionId = 8,
                    PersonaId = 8,
                    Zona = "Sur",
                    Calle = "El Cedro",
                    Numero = "753",
                    NombreEdificio = "Condominio Mar",
                    Referencia = "Cerca del centro comercial",
                    Ubicacion = ""
                },

                new Direccion
                {
                    DireccionId = 9,
                    PersonaId = 9,
                    Zona = "Este",
                    Calle = "La Paz",
                    Numero = "852",
                    NombreEdificio = "La Radial",
                    Referencia = "Detras del parque",
                    Ubicacion = ""
                },

                new Direccion
                {
                    DireccionId = 10,
                    PersonaId = 10,
                    Zona = "Oeste",
                    Calle = "El Roble",
                    Numero = "456",
                    NombreEdificio = "Villa Verde",
                    Referencia = "Frente a la plaza central",
                    Ubicacion = ""
                }
            );

            //Datos iniciales para Productos
            builder.Entity<Producto>().HasData
            (
                new Producto
                {
                    ProductoId = 1,
                    Categoria = "Panaderia",
                    Nombre = "Pan Frances",
                    Descripcion = "Pan fresco elaborado diariamente",
                    Precio = 1.50m,
                    Fotografia = "/img/pan-frances.jpg",
                    Unidad = "Unidad",
                    Disponible = true
                },

                new Producto
                {
                    ProductoId = 2,
                    Categoria = "Panaderia",
                    Nombre = "Marraqueta",
                    Descripcion = "Clasica marraqueta crujiente",
                    Precio = 1.00m,
                    Fotografia = "/img/marraqueta.jpg",
                    Unidad = "Unidad",
                    Disponible = true
                },

                new Producto
                {
                    ProductoId = 3,
                    Categoria = "Pasteleria",
                    Nombre = "Torta de Chocolate",
                    Descripcion = "Torta humeda con cobertura de chocolate",
                    Precio = 35.00m,
                    Fotografia = "/img/torta-chocolate.jpg",
                    Unidad = "Unidad",
                    Disponible = true
                },

                new Producto
                {
                    ProductoId = 4,
                    Categoria = "Pasteleria",
                    Nombre = "Rollo de Canela",
                    Descripcion = "Rollo suave con glaseado dulce",
                    Precio = 4.50m,
                    Fotografia = "/img/rollo-canela.jpg",
                    Unidad = "Unidad",
                    Disponible = true
                },

                new Producto
                {
                    ProductoId = 5,
                    Categoria = "Bebidas",
                    Nombre = "Cafe Americano",
                    Descripcion = "Cafe recien pasado",
                    Precio = 5.00m,
                    Fotografia = "/img/cafe-americano.jpg",
                    Unidad = "Vaso",
                    Disponible = true
                }
            );

            //Datos iniciales para Producciones
            builder.Entity<Produccion>().HasData
            (
                new Produccion
                {
                    ProduccionId = 1,
                    ProductoId = 1, // Pan Frances
                    NumeroProduccion = "PRD-001",
                    FechaProduccion = new DateTime(2025, 1, 10),
                    FechaVencimiento = new DateTime(2025, 1, 12),
                    DiasValidos = 2,
                    NumeroLote = "L-001",
                    CantidadProducida = 200,
                    CantidadDisponible = 200
                },

                new Produccion
                {
                    ProduccionId = 2,
                    ProductoId = 2, // Marraqueta
                    NumeroProduccion = "PRD-002",
                    FechaProduccion = new DateTime(2025, 1, 10),
                    FechaVencimiento = new DateTime(2025, 1, 12),
                    DiasValidos = 2,
                    NumeroLote = "L-002",
                    CantidadProducida = 350,
                    CantidadDisponible = 350
                },

                new Produccion
                {
                    ProduccionId = 3,
                    ProductoId = 3, // Torta de Chocolate
                    NumeroProduccion = "PRD-003",
                    FechaProduccion = new DateTime(2025, 1, 5),
                    FechaVencimiento = new DateTime(2025, 1, 15),
                    DiasValidos = 10,
                    NumeroLote = "L-003",
                    CantidadProducida = 15,
                    CantidadDisponible = 15
                },

                new Produccion
                {
                    ProduccionId = 4,
                    ProductoId = 4, // Rollo de Canela
                    NumeroProduccion = "PRD-004",
                    FechaProduccion = new DateTime(2025, 1, 9),
                    FechaVencimiento = new DateTime(2025, 1, 14),
                    DiasValidos = 5,
                    NumeroLote = "L-004",
                    CantidadProducida = 50,
                    CantidadDisponible = 50
                },

                new Produccion
                {
                    ProduccionId = 5,
                    ProductoId = 5, // Cafe Americano
                    NumeroProduccion = "PRD-005",
                    FechaProduccion = new DateTime(2025, 1, 10),
                    FechaVencimiento = new DateTime(2025, 1, 20),
                    DiasValidos = 10,
                    NumeroLote = "L-005",
                    CantidadProducida = 100,
                    CantidadDisponible = 100
                }
            );

        }
    }
}
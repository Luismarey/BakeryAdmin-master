# 🥖 BakeryAdmin: Sistema de Gestión Integral para Panaderías

## 📝 Descripción del Proyecto

**BakeryAdmin** es un sistema de administración web integral diseñado para gestionar las operaciones diarias de una panadería o repostería. Permite manejar de forma centralizada productos, pedidos, producción, clientes, empleados y proveedores.

Este proyecto está desarrollado con el patrón **Modelo-Vista-Controlador (MVC)** en **ASP.NET Core** y utiliza **Entity Framework Core** para la persistencia de datos.

---

## ✨ Módulos y Funcionalidades

El sistema incluye los siguientes módulos principales:

* **Gestión de Productos:**
    * CRUD completo para el catálogo de productos de la panadería.
* **Gestión de Órdenes (Pedidos):**
    * Creación y seguimiento de órdenes de venta.
    * Registro detallado de los ítems de la orden.
* **Gestión de Producción:**
    * Control y registro de las producciones de productos por lotes.
* **Gestión de Personas:**
    * Administración de **Clientes**, **Empleados**, **Proveedores** y **Vendedores**.
    * Soporte para múltiples **Direcciones** por persona.
* **Sistema de Pago:**
    * Implementación de múltiples métodos de pago (Efectivo, Tarjeta, QR) utilizando un patrón de diseño (Strategy Pattern).
* **Seguridad:**
    * Autenticación y autorización de usuarios mediante **ASP.NET Core Identity**.
* **Registro de Cajas:**
    * Control de transacciones y aperturas/cierres de caja.

---

## 🛠️ Tecnologías

### Backend
* **Lenguaje:** C#
* **Framework:** ASP.NET Core 8.0 (MVC)
* **ORM:** Entity Framework Core
* **Base de Datos:** Microsoft SQL Server
* **Autenticación:** ASP.NET Core Identity

### Frontend
* HTML5, CSS3, JavaScript
* jQuery y librerías de validación (`jquery-validate`, `jquery-validation-unobtrusive`).

---

## 🚀 Instrucciones de Configuración e Instalación

### 📋 Requisitos Previos

Asegúrate de tener instalado:

* **SDK de .NET 8.0** o superior.
* **Microsoft SQL Server** (o SQL Server LocalDB).
* Un IDE (como **Visual Studio** o **Visual Studio Code**).

### ⚙️ Pasos de Instalación

1.  **Clonar el repositorio:**
    ```bash
    git clone [URL_DEL_REPOSITORIO]
    cd BakeryAdmin
    ```

2.  **Configurar la Conexión a la Base de Datos:**
    Abre el archivo `appsettings.json` y verifica la cadena de conexión `DefaultConnection`.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BakeryAdminDB;Trusted_Connection=True;MultipleActiveResultSets=true" 
      // Ajusta esta cadena si tu configuración de SQL Server es diferente.
    }
    ```

3.  **Aplicar Migraciones de Entity Framework Core:**
    Ejecuta el siguiente comando en la terminal (CLI de .NET) dentro de la carpeta del proyecto para crear la base de datos y sus tablas:

    ```bash
    dotnet ef database update
    ```
    *Asegúrate de que la CLI de Entity Framework Core esté instalada: `dotnet tool install --global dotnet-ef`.*

4.  **Ejecutar la Aplicación:**
    Inicia la aplicación desde tu IDE o utilizando el comando de la CLI:

    ```bash
    dotnet run
    ```
    La aplicación estará accesible a través de la URL que te indique el sistema (ej.: `https://localhost:7000`).

---

## 📖 Uso Básico

1.  **Acceso:** Navega a la URL local de la aplicación. Serás dirigido a la página de **Login**.
2.  **Registro:** Si es la primera ejecución, regístrate para crear el primer usuario (administrador).
3.  **Navegación:** Utiliza el menú para acceder a los diferentes módulos de administración:
    * **/Productos:** Para la gestión de inventario.
    * **/Ordenes:** Para la toma y seguimiento de pedidos.
    * **/Personas:** Para administrar clientes, empleados y proveedores.
    * **/Producciones:** Para registrar lotes de fabricación.

---

## 📬 Contribución

¡Las contribuciones que mejoren este sistema son bienvenidas! Si deseas contribuir, por favor sigue estos pasos:

1.  Haz un *fork* del repositorio.
2.  Crea una rama de característica (`git checkout -b feature/nombre-caracteristica`).
3.  Realiza tus cambios.
4.  Haz *commit* de tus cambios (`git commit -m 'feat: Añadir nueva característica X'`).
5.  Sube tus cambios a tu *fork* (`git push origin feature/nombre-caracteristica`).
6.  Abre un *Pull Request*.

---

## ⚖️ Licencia

Este proyecto está bajo la licencia [**BakeryAdmin_master**].

The project will create a LocalDB database named `BakeryAdminDb` automatically.

Default seeded admin:
- email: admin@bakery.local
- password: Admin#1234


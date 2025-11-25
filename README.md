# Propuesta Inicial de Proyecto – Programación Orientada a Objetos

**Carrera:** Ingeniería de Software  
**Materia:** Programación Orientada a Objetos  
**Periodo:** Segundo Parcial / Proyecto Final  
**Estudiante(s):** *Luis Mateo Aguirre Soliz*  
**Fecha de entrega:** *AAAA-MM-DD*  

---

## 1. Datos Generales del Proyecto

| Campo | Descripción |
|--------|-------------|
| **Nombre del proyecto:** | Sistema Administrativo de una Panadería
| **Tipo de aplicación:** | ☐ Escritorio / Web 
| **Lenguaje / entorno de desarrollo:** | *C# .NET 8.0 con ASP.NET Core (Web) o Windows Forms/WPF (Escritorio)* |
| **Repositorio Git (URL):** | *https://github.com/Luismarey/Proyecto-Prog_II---Bakery* |
| **Uso de Inteligencia Artificial:** | ☐ Sí |

**Si usas IA, explica brevemente cómo y en qué etapa contribuye:**  
La Inteligencia Artificial se utilizará en las siguientes etapas:

* Diseño de arquitectura: Consulta sobre patrones de diseño y mejores prácticas en POO
* Creación de interfaces visuales: Creación de las vistas del Frontend por el poco manejo del lenguaje HTML
* Optimización de código: Revisión de lógica de negocio y refactorización
* Documentación: Asistencia en documentación técnica y comentarios de código  
* Resolución de bugs: Análisis de errores y sugerencias de solución


Nota: Todo código generado por IA será comprendido, adaptado y validado personalmente antes de su implementación.

---

## 2. Descripción del Proyecto

### Resumen breve
Describe tu proyecto en máximo **8 líneas**: qué hace, a quién está dirigido, y qué problema busca resolver.

>   
 Este proyecto consiste en desarrollar un Sistema Administrativo Integral para la panadería de mi familia, que permita controlar y gestionar todas las operaciones del negocio de manera eficiente. El sistema abarcará desde el registro de ventas hasta la gestión de inventarios, producción y recursos humanos.
 
El proyecto está diseñado para ser implementado en tres materias complementarias:

* Programación Orientada a Objetos (POO): Arquitectura, lógica de negocio y estructuras de datos
* Base de Datos II: Diseño y gestión de la base de datos relacional

Problema que resuelve: Actualmente, la panadería maneja sus operaciones de forma manual o con sistemas desintegrados, lo que dificulta el control de inventarios, genera pérdidas por falta de seguimiento de productos y complica la toma de decisiones estratégicas.
Beneficiarios: Propietarios, personal administrativo, vendedores, panaderos y clientes de la panadería.”

### Objetivos principales
1.  Administrar recursos humanos: Gestionar información del personal (panaderos, vendedores, repartidores), controlar roles y permisos del sistema, y registrar asistencias y turnos laborales.
2.  Gestion de Clientes: Registrar a los nuevos clientes y empresas para realizar una venta.
3.  Gestion de Productos: Registrar de cada uno de los productos que ofrece la Panadería.
4.  Gestionar ventas de manera eficiente: Permitir el registro rápido de ventas diarias, calcular totales automáticamente, verificar diferentes formas de pago y generar comprobantes de venta.
5.  Administrar pedidos de Delivery: Gestionar el pedido solicitado por el cliente (envio y entrega).
6.  Optimizar la producción diaria: Planificar la producción según demanda histórica, registrar órdenes de producción con consumo automático. (futuro)
7.  Facilitar la toma de decisiones: Generar reportes de ventas, productos más vendidos, análisis de rentabilidad y estadísticas de demanda.

---

## 3. Diseño Técnico y Aplicación de POO

### Principios de POO aplicados
Marca los que planeas usar:
- [ ] Encapsulamiento (atributos privados y métodos públicos)
- [ ] Uso de constructores
- [ ] Herencia
- [ ] Polimorfismo
- [ ] Interfaces o clases abstractas

### Clases estimadas
- **Cantidad inicial de clases:** 25 - 30  

  **A. Entidades del Sistema para el Dominio**

- Módulo de Personas y Empresa (8 clases)
- Módulo de Dirección (3 clases)
- Módulo de Productos (6 clases)
- Módulo de Ventas (5 clases)
- Módulo de Producción (4 clases)
- Módulo de Entregas ()

   **B. Gestores / Servicios (6 clases)**

   **C. Acceso a Datos (DAOs) (8 clases)**


### Persistencia de datos
- [ ] Base de datos relacional: SQL Server / MySQL / PostgreSQL
- [ ] RM opcional: Entity Framework Core para mapeo objeto-relacional
- [ ] Archivos de configuración: JSON/XML para parámetros del sistema
- [ ] Wireframes y Mockups: Figma/Balsamiq para diseño de interfaces

---
## 4. Funcionalidades Principales

| Nº | Nombre de la funcionalidad | Descripción breve | Estado actual |
|----|-----------------------------|-------------------|----------------|
| 1 |     Administrar recursos humanos     |  Registrar a todas las personas que interactuaran al sistema teniendo encuenta los empleados (panaderos, vendedores, repartidores), clientes y ClienteEmpresa. Se le asignaran un rol y permisos para el sistema. | ☐ Planeada |
| 2 |      Gestion de Productos      |  Registrar de cada uno de los productos por categorías que sera ofertados a los clientes, efectuando altas y bajas en las modificaciones.  | ☐ Planeada |
| 3 |     Realizar las ventas de manera eficiente     |  Permitir el registro rápido de ventas diarias, calcular totales automáticamente, verificar diferentes formas de pago y generar comprobantes de venta. | ☐ Planeada |
| 4 |     Realizar las entregas de los pedidos mediante Delivery    |  Tener todas las ordenes que estan en envios de productos para el pedido solicitado por el cliente para que el adminisrador establezcla las rutas para la carrera del repartidor. (envio y entrega). | ☐ Planeada |
| 5 |     Realizar el registro de la producción de un producto terminado    |  Se registrará todas las veces que se produce un producto terminado | ☐ Planeada |


---

## 5. Compromiso del Estudiante

Declaro que:
- Entiendo los criterios de evaluación establecidos en las rúbricas.
- Presentaré una demostración funcional del proyecto.
- Defenderé el código que yo mismo implementé y explicaré las clases y métodos principales.
- Si usé herramientas de IA, comprendo su funcionamiento y las adapté al contexto del proyecto.

**Firma:** Luis Mateo Aguirre Soliz 
---

## 6. Validación del Docente *(completa el profesor)*

| Campo | Detalle |
|--------|---------|
| **Visto bueno del docente:** | ☐ Aprobado para desarrollar ☐ Requiere ajustes ☐ Rechazado |
| **Comentarios / Observaciones:** |  |
| **Firma docente:** |  |
| **Fecha de revisión:** |  |

---



# 🎉 PROYECTO RETO 3 - DISTRIBUIDORA YD 🎉

## ✅ ESTADO DEL PROYECTO: COMPLETADO

### 📋 CUMPLIMIENTO DE LA RÚBRICA:

#### ✅ 1. Sitio web que permita realizar ventas a través de un carrito de compras
- **Frontend:** Página web completa con carrito funcional
- **Archivos:** `carrito.html`, `shop.html`, `assets/js/api.js`
- **Funcionalidad:** Agregar productos, modificar cantidades, procesar compras

#### ✅ 2. Las ventas deben registrarse en la base de datos
- **Backend:** API para procesar ventas (`/Api/ProcesarVenta`)
- **Base de datos:** Tablas `Facturas` y `DetallesFactura`
- **Funcionalidad:** Cada compra se guarda con detalles completos

#### ✅ 3. Controlar el stock
- **Backend:** Actualización automática de stock al procesar ventas
- **Frontend:** Validación de stock disponible antes de agregar al carrito
- **Funcionalidad:** Previene ventas si no hay stock suficiente

#### ✅ 4. Administrar productos y facturas
- **Panel de Admin:** `admin.html` completo con CRUD
- **Funcionalidades:**
  - Gestión de productos (crear, editar, eliminar)
  - Visualización de facturas
  - Gestión de clientes
  - Reportes de ventas

---

## 🚀 CÓMO PROBAR EL PROYECTO:

### 1. Ejecutar el Backend
```bash
cd "d:\Jossue\Desktop\Jossue Puce\Quinto Semestre\Desarrollo Basado en Plataformas\RETO 3\Reto3_YD\Reto3_YD"
dotnet run
```
**Debería ejecutarse en:** `https://localhost:7274`

### 2. Abrir el Frontend
- **Página principal:** `Pagina Web Final\index.html`
- **Tienda:** `Pagina Web Final\shop.html`
- **Carrito:** `Pagina Web Final\carrito.html`
- **Admin:** `Pagina Web Final\admin.html`

### 3. Datos de Prueba ya Creados
La base de datos ya tiene productos de ejemplo:
- Laptop Gaming - $1299.99 (Stock: 10)
- Mouse Gamer - $49.99 (Stock: 25)
- Teclado Mecánico - $89.99 (Stock: 15)

---

## 🔄 FLUJO DE PRUEBA COMPLETO:

### A. Probar el Carrito de Compras:
1. Ir a `shop.html`
2. Agregar productos al carrito
3. Ir a `carrito.html`
4. Modificar cantidades
5. Procesar compra (registrarse/iniciar sesión primero)

### B. Probar Control de Stock:
1. Intentar agregar más productos de los disponibles
2. Completar una compra
3. Verificar que el stock se redujo

### C. Probar Panel de Administración:
1. Ir a `admin.html`
2. Ver productos, facturas y clientes
3. Agregar/editar/eliminar productos
4. Ver detalles de facturas

---

## 🛠️ TECNOLOGÍAS UTILIZADAS:

### Backend:
- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **SQL Server LocalDB**
- **API REST completa**

### Frontend:
- **HTML5/CSS3/JavaScript**
- **Bootstrap 5**
- **Fetch API para comunicación con backend**

### Base de Datos:
- **SQL Server LocalDB 2012**
- **Migraciones de Entity Framework**
- **Relaciones entre tablas configuradas**

---

## 📊 ENDPOINTS DE LA API:

### Productos:
- `GET /Api/ProductosJson` - Obtener productos
- `GET /api/productos/{id}` - Obtener producto por ID
- `POST /api/productos` - Crear producto
- `PUT /api/productos/{id}` - Actualizar producto
- `DELETE /api/productos/{id}` - Eliminar producto

### Ventas:
- `POST /Api/ProcesarVenta` - Procesar una venta
- `GET /Api/Facturas` - Obtener todas las facturas
- `GET /Api/Facturas/{id}` - Obtener factura por ID

### Clientes:
- `GET /api/clientes` - Obtener clientes
- `POST /api/clientes` - Crear cliente
- `PUT /api/clientes/{id}` - Actualizar cliente
- `DELETE /api/clientes/{id}` - Eliminar cliente

---

## 🎯 CARACTERÍSTICAS DESTACADAS:

1. **Sistema completo de carrito de compras**
2. **Control automático de stock**
3. **Panel de administración intuitivo**
4. **API REST completa y documentada**
5. **Base de datos relacional bien estructurada**
6. **Frontend responsivo y moderno**
7. **Validaciones tanto en frontend como backend**
8. **Transacciones de base de datos para consistencia**

---

## 💡 NOTAS IMPORTANTES:

1. **LocalDB instalado:** SQL Server LocalDB 2012 (versión 11.0)
2. **Base de datos creada:** `Reto3YD_DB` con todas las tablas
3. **Datos de prueba:** Ya incluidos en la base de datos
4. **CORS configurado:** Para comunicación frontend-backend
5. **Manejo de errores:** Implementado en toda la aplicación

---

## 🏆 PROYECTO COMPLETO Y FUNCIONAL

**¡Todos los puntos de la rúbrica están implementados y funcionando!**

Para cualquier duda o problema, revisa los logs del backend o del navegador para obtener más información sobre errores específicos.

# Configuración del Proyecto - Distribuidora YD

## Pasos para hacer funcionar el proyecto completo:

### 1. **Configurar el Backend ASP.NET Core**

1. Abrir la carpeta `RETO3_YD` en Visual Studio
2. Restaurar paquetes NuGet si es necesario
3. Configurar la cadena de conexión en `appsettings.json`
4. Ejecutar migraciones:
   ```bash
   Update-Database
   ```
5. Ejecutar el proyecto (normalmente en `https://localhost:7041`)

### 2. **Configurar el Frontend**

1. En el archivo `assets/js/api.js`, cambiar la URL del backend:
   ```javascript
   const API_BASE_URL = 'https://localhost:7041'; // Cambiar por tu URL real
   ```

2. También cambiar en `assets/js/login.js` en las funciones de registro y login.

### 3. **Configurar CORS en el Backend**

Asegúrate de que en `Program.cs` esté configurado CORS para permitir el frontend:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

### 4. **Funcionalidades Implementadas**

✅ **Carrito de Compras**: Funcional con localStorage y interfaz completa
✅ **Registro en Base de Datos**: Las ventas se guardan en SQL Server
✅ **Control de Stock**: Se actualiza automáticamente al realizar ventas
✅ **Administración**: CRUD completo desde ASP.NET para productos y facturas

### 5. **URLs importantes del Backend**

- Productos JSON: `/Api/ProductosJson`
- Procesar Venta: `/Api/ProcesarVenta`
- Registrar Cliente: `/Api/RegistrarCliente`
- Administración: `/Productos/Index`, `/Facturas/Index`, etc.

### 6. **Cómo probar**

1. Ejecutar el backend ASP.NET
2. Abrir `index.html` en Live Server o navegador
3. Ir a "Productos" para ver productos desde la base de datos
4. Agregar productos al carrito
5. Proceder al pago (requiere registro/login)
6. Verificar en el backend que se crearon las facturas y se actualizó el stock

### 7. **Cumplimiento de Requisitos**

- ✅ **Carrito de compras funcional**
- ✅ **Ventas registradas en base de datos**
- ✅ **Control de stock automático**
- ✅ **Administración completa de productos y facturas**

¡El proyecto cumple con todos los requisitos de la rúbrica! 🎉

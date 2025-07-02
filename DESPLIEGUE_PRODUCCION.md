# 🚀 Guía de Despliegue para Producción

## 📋 Requisitos del Servidor

### Backend (ASP.NET Core)
- Windows Server 2016+ o Windows 10+
- .NET 8.0 Runtime
- SQL Server o SQL Server Express
- IIS (opcional, recomendado para producción)

### Frontend (HTML/CSS/JS)
- Servidor web (IIS, Apache, Nginx)
- Navegadores compatibles con ES6+

## 🔧 Pasos de Instalación

### 1. Preparar el Servidor Backend

#### Opción A: Usando IIS (Recomendado)
1. **Instalar IIS y ASP.NET Core Module**:
   ```
   - Panel de Control → Programas → Activar características de Windows
   - Marcar: Internet Information Services
   - Descargar ASP.NET Core Hosting Bundle desde Microsoft
   ```

2. **Configurar Application Pool**:
   ```
   - Abrir IIS Manager
   - Crear nuevo Application Pool
   - .NET CLR Version: No Managed Code
   - Managed Pipeline Mode: Integrated
   ```

3. **Publicar la aplicación**:
   ```bash
   # Ejecutar en la carpeta del proyecto
   instalar_produccion.bat
   ```

4. **Configurar sitio en IIS**:
   ```
   - Crear nuevo sitio web en IIS
   - Physical Path: apuntar a la carpeta 'publish'
   - Binding: Port 5210 (o el puerto deseado)
   - Application Pool: el creado anteriormente
   ```

#### Opción B: Ejecución directa
1. **Ejecutar script de instalación**:
   ```bash
   instalar_produccion.bat
   ```

2. **Iniciar aplicación**:
   ```bash
   cd publish
   dotnet Reto3_YD.dll
   ```

### 2. Configurar Base de Datos

#### SQL Server LocalDB (Desarrollo/Pruebas)
```bash
# El script de instalación lo hace automáticamente
sqlcmd -S (localdb)\MSSQLLocalDB -Q "CREATE DATABASE Reto3YD_DB;"
dotnet ef database update
```

#### SQL Server Express/Full (Producción)
1. **Actualizar cadena de conexión** en `appsettings.Production.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=NOMBRE_SERVIDOR;Database=Reto3YD_DB;Integrated Security=true;TrustServerCertificate=true;"
     }
   }
   ```

2. **Aplicar migraciones**:
   ```bash
   dotnet ef database update --environment Production
   ```

### 3. Desplegar Frontend

#### IIS
1. **Copiar archivos**:
   ```
   - Copiar toda la carpeta "Pagina Web Final" al servidor
   - Colocar en C:\inetpub\wwwroot\mi-tienda (o ubicación deseada)
   ```

2. **Configurar sitio en IIS**:
   ```
   - Crear nuevo sitio web
   - Physical Path: apuntar a la carpeta copiada
   - Port: 80 (HTTP) o 443 (HTTPS)
   ```

3. **Actualizar configuración**:
   - Editar `assets/js/config.js`
   - Cambiar `TU_IP_SERVIDOR` por la IP real del servidor backend

#### Apache
1. **Copiar archivos** al directorio web de Apache
2. **Configurar Virtual Host**:
   ```apache
   <VirtualHost *:80>
       DocumentRoot "C:/xampp/htdocs/mi-tienda"
       ServerName mi-tienda.local
   </VirtualHost>
   ```

## 🌐 Configuración de Red

### Puertos necesarios
- **Backend**: 5210 (HTTP) - configurable
- **Frontend**: 80 (HTTP) / 443 (HTTPS)
- **Base de datos**: 1433 (SQL Server) - solo interno

### Firewall
```bash
# Windows Firewall - Permitir puertos
netsh advfirewall firewall add rule name="Backend API" dir=in action=allow protocol=TCP localport=5210
netsh advfirewall firewall add rule name="Web HTTP" dir=in action=allow protocol=TCP localport=80
```

## 🔑 Variables de Entorno

### Backend
```bash
set ASPNETCORE_ENVIRONMENT=Production
set ASPNETCORE_URLS=http://0.0.0.0:5210
```

### Frontend
Editar `config.js`:
```javascript
production: {
    API_BASE_URL: 'http://192.168.1.100:5210' // IP real del servidor
}
```

## 🔍 Verificación de Funcionamiento

### 1. Backend
```bash
# Verificar que la API responde
curl http://localhost:5210/Api/ProductosJson
```

### 2. Frontend
```bash
# Abrir en navegador
http://IP_SERVIDOR/index.html
```

### 3. Conexión Frontend-Backend
- Abrir DevTools (F12) en el navegador
- Verificar que no hay errores de CORS
- Confirmar que los productos se cargan correctamente

## 🚨 Solución de Problemas

### Error de CORS
```csharp
// En Program.cs, verificar configuración CORS:
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

### Error de Base de Datos
```bash
# Verificar conexión
sqlcmd -S SERVIDOR -Q "SELECT 1"

# Aplicar migraciones manualmente
dotnet ef database update --verbose
```

### Error 404 en API
- Verificar que el backend esté ejecutándose
- Confirmar el puerto correcto
- Revisar logs de la aplicación

## 📁 Estructura de Archivos para Producción

```
Servidor Backend:
├── publish/
│   ├── Reto3_YD.dll
│   ├── appsettings.Production.json
│   └── ...otros archivos...

Servidor Frontend:
├── wwwroot/
│   ├── index.html
│   ├── shop.html
│   ├── assets/
│   │   ├── css/
│   │   ├── js/
│   │   │   ├── config.js (ACTUALIZADO)
│   │   │   └── api.js
│   │   └── img/
│   └── ...otros archivos...
```

## 📞 Contacto

Para problemas de despliegue, verificar:
1. Logs de la aplicación
2. Event Viewer de Windows
3. Logs de IIS (si aplica)
4. Consola del navegador (F12)

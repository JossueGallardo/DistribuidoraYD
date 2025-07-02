# Reto3_YD - Proyecto ASP.NET Core

## Descripción
Aplicación web ASP.NET Core con Entity Framework y base de datos para gestión de productos, clientes y ventas.

## Características
- ✅ ASP.NET Core 8.0
- ✅ Entity Framework Core
- ✅ SQL Server (desarrollo) / PostgreSQL (producción)
- ✅ API REST
- ✅ Razor Pages
- ✅ CORS configurado
- ✅ Migraciones automáticas

## Despliegue en Railway

### Configuración automática
Este proyecto está configurado para desplegarse automáticamente en Railway con:
- ✅ Dockerfile optimizado
- ✅ Configuración de PostgreSQL automática
- ✅ Variables de entorno manejadas automáticamente
- ✅ Puerto dinámico configurado

### Variables de entorno necesarias (automáticas en Railway):
- `DATABASE_URL` - Se configura automáticamente con PostgreSQL
- `PORT` - Se configura automáticamente
- `ASPNETCORE_ENVIRONMENT=Production`

## Desarrollo local

### Prerrequisitos
- .NET 8.0 SDK
- SQL Server LocalDB

### Ejecutar localmente
```bash
dotnet restore
dotnet ef database update
dotnet run
```

## Estructura del proyecto
- `/Controllers` - Controladores de API
- `/Models` - Modelos de datos
- `/Data` - Contexto de Entity Framework
- `/Pages` - Razor Pages
- `/Migrations` - Migraciones de base de datos
- `/wwwroot` - Archivos estáticos

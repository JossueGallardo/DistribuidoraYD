# Troubleshooting Guide para Railway

## 🚨 Problemas Comunes y Soluciones

### 1. Application Crashed
**Síntomas**: La aplicación muestra "crashed" en Railway

**Soluciones**:
1. **Verificar Logs en Railway**:
   - Ve a tu proyecto en Railway
   - Haz clic en la pestaña "Deployments"
   - Revisa los logs de build y runtime

2. **Problemas Comunes**:
   - **Puerto incorrecto**: Asegúrate de usar la variable `PORT`
   - **Base de datos no conectada**: Verifica que PostgreSQL esté creado
   - **Migraciones fallidas**: Logs mostrarán errores de EF Core

### 2. Errores de Base de Datos
**Síntomas**: Errores de conexión a PostgreSQL

**Soluciones**:
1. **Verificar PostgreSQL Service**:
   - En Railway, asegúrate de tener un servicio PostgreSQL
   - Verifica que `DATABASE_URL` esté configurada

2. **Variables de Entorno**:
   ```
   DATABASE_URL=postgresql://user:pass@host:port/db
   PORT=8080
   ASPNETCORE_ENVIRONMENT=Production
   ```

### 3. Errores de Compilación
**Síntomas**: Build failed en Railway

**Soluciones**:
1. **Verificar Dockerfile**:
   - Dockerfile debe estar en la raíz del repositorio
   - Verificar que `Reto3_YD.csproj` exista

2. **Dependencias**:
   - Asegúrate de que todos los paquetes NuGet estén en el .csproj
   - Verifica que no haya referencias rotas

### 4. Health Check Failures
**Síntomas**: Railway no puede hacer health check

**Soluciones**:
1. **Endpoint disponible**: GET `/health` debe retornar 200
2. **Puerto correcto**: App debe escuchar en `0.0.0.0:$PORT`

## 🔧 Comandos de Diagnóstico Local

```bash
# Verificar compilación
dotnet build --configuration Release

# Verificar publicación
dotnet publish --configuration Release

# Probar localmente con variables de entorno
set ASPNETCORE_ENVIRONMENT=Production
set PORT=8080
dotnet run
```

## 📊 Verificaciones en Railway

1. **Variables de Entorno**:
   - `PORT`: Configurado automáticamente
   - `DATABASE_URL`: Debe existir si hay PostgreSQL
   - `ASPNETCORE_ENVIRONMENT`: Debe ser "Production"

2. **Servicios Requeridos**:
   - ✅ Web Service (tu aplicación)
   - ✅ PostgreSQL Database

3. **Logs a Revisar**:
   - Build logs: Errores de compilación
   - Deploy logs: Errores de inicio
   - Application logs: Errores de runtime

## 🚀 Pasos de Recuperación

1. **Re-deploy**:
   ```bash
   git add .
   git commit -m "Fix: Configuración corregida"
   git push origin main
   ```

2. **Restart Services**:
   - En Railway, reinicia el servicio web
   - Reinicia PostgreSQL si es necesario

3. **Clean Build**:
   - Elimina el deployment fallido
   - Crea nuevo deployment desde GitHub

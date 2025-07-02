# Distribuidora YD - Deployment en Railway

## 🚀 Guía Completa para Desplegar en Railway

### Requisitos Previos
- Cuenta en [Railway](https://railway.app)
- Cuenta en [GitHub](https://github.com)
- El proyecto ya está configurado para PostgreSQL en producción

### Pasos para el Despliegue

#### 1. Conectar tu repositorio local con GitHub

```bash
# Agregar todos los archivos al repositorio
git add .

# Hacer commit de los cambios
git commit -m "Configuración inicial para Railway"

# Conectar con tu repositorio remoto
git remote add origin https://github.com/JossueGallardo/DistribuidoraYD.git

# Subir los cambios
git push -u origin main
```

#### 2. Crear proyecto en Railway

1. Ve a [railway.app](https://railway.app)
2. Inicia sesión con tu cuenta de GitHub
3. Haz clic en "New Project"
4. Selecciona "Deploy from GitHub repo"
5. Busca y selecciona tu repositorio `DistribuidoraYD`
6. Railway detectará automáticamente que es un proyecto .NET

#### 3. Configurar Base de Datos PostgreSQL

1. En tu proyecto de Railway, haz clic en "New Service"
2. Selecciona "Database" → "PostgreSQL"
3. Railway creará automáticamente una base de datos PostgreSQL
4. La variable `DATABASE_URL` se configurará automáticamente

#### 4. Variables de Entorno (Automáticas)

Railway configurará automáticamente:
- `DATABASE_URL`: URL de conexión a PostgreSQL
- `PORT`: Puerto dinámico asignado por Railway
- `ASPNETCORE_ENVIRONMENT`: Se configurará como "Production"

#### 5. Verificar el Despliegue

1. Railway comenzará a construir tu aplicación usando el Dockerfile
2. El proceso tomará unos minutos
3. Una vez completado, tendrás una URL pública para tu aplicación
4. Las migraciones se aplicarán automáticamente en el primer despliegue

### 🔧 Características Configuradas

- ✅ **Docker**: Dockerfile optimizado para .NET 8.0
- ✅ **Base de Datos**: PostgreSQL en producción, SQL Server en desarrollo
- ✅ **Migraciones**: Se aplican automáticamente en producción
- ✅ **CORS**: Configurado para permitir acceso desde cualquier origen
- ✅ **Puerto Dinámico**: Usa la variable PORT de Railway
- ✅ **SSL**: Configurado para PostgreSQL con SSL requerido

### 📁 Archivos Importantes para Railway

- `Dockerfile`: Configuración de contenedor
- `railway.json`: Configuración específica de Railway
- `.dockerignore`: Archivos a ignorar en el build
- `Program.cs`: Configurado para PostgreSQL en producción

### 🌐 Acceso a la Aplicación

Una vez desplegado, tu aplicación estará disponible en:
- URL principal: `https://tu-app.railway.app`
- API endpoints: `https://tu-app.railway.app/api/[controller]`

### 🔗 Enlaces de la API

- **Productos**: `/api/Productos`
- **Clientes**: `/api/Clientes`
- **Ventas**: `/api/Ventas`

### 📊 Monitoreo

En el dashboard de Railway puedes:
- Ver logs en tiempo real
- Monitorear uso de recursos
- Ver métricas de la aplicación
- Configurar alertas

### 🛠️ Troubleshooting

Si encuentras problemas:
1. Revisa los logs en Railway Dashboard
2. Verifica que todas las variables de entorno estén configuradas
3. Asegúrate de que el Dockerfile esté en la raíz del proyecto
4. Confirma que las migraciones sean compatibles con PostgreSQL

## 📞 Soporte

Si necesitas ayuda, revisa:
- [Documentación de Railway](https://docs.railway.app)
- Logs de la aplicación en Railway Dashboard
- Variables de entorno en el proyecto

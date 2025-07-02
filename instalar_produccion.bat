@echo off
echo =================================================
echo    SCRIPT DE INSTALACION PARA PRODUCCION
echo =================================================
echo.

echo 1. Verificando requisitos...

REM Verificar si .NET Core está instalado
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET Core no está instalado
    echo Descarga e instala .NET 8.0 Runtime desde:
    echo https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo ✓ .NET Core detectado

REM Verificar si SQL Server está disponible
sqlcmd -S (localdb)\MSSQLLocalDB -Q "SELECT 1" >nul 2>&1
if %errorlevel% neq 0 (
    echo ADVERTENCIA: SQL Server LocalDB no detectado
    echo Necesitarás configurar la cadena de conexión manualmente
)

echo.
echo 2. Configurando base de datos...
echo.

REM Crear base de datos si no existe
sqlcmd -S (localdb)\MSSQLLocalDB -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Reto3YD_DB') CREATE DATABASE Reto3YD_DB;"

echo ✓ Base de datos verificada

echo.
echo 3. Aplicando migraciones...
echo.

REM Aplicar migraciones
dotnet ef database update

echo ✓ Migraciones aplicadas

echo.
echo 4. Publicando aplicación...
echo.

REM Limpiar publicación anterior
if exist "publish" rmdir /s /q "publish"

REM Publicar aplicación
dotnet publish -c Release -o publish --self-contained false

echo ✓ Aplicación publicada

echo.
echo 5. Configurando variables de entorno...
echo.

REM Crear archivo de configuración de entorno
echo ASPNETCORE_ENVIRONMENT=Production > publish\environment.txt
echo ASPNETCORE_URLS=http://0.0.0.0:5210 >> publish\environment.txt

echo ✓ Variables configuradas

echo.
echo =================================================
echo           INSTALACION COMPLETADA
echo =================================================
echo.
echo Para ejecutar la aplicación:
echo 1. Navega a la carpeta 'publish'
echo 2. Ejecuta: dotnet Reto3_YD.dll
echo.
echo Para IIS:
echo 1. Copia la carpeta 'publish' al servidor IIS
echo 2. Configura un Application Pool con .NET Core
echo 3. Apunta el sitio web a la carpeta 'publish'
echo.
echo Para el frontend:
echo 1. Copia la carpeta 'Pagina Web Final' a tu servidor web (IIS/Apache)
echo 2. Actualiza la IP del servidor en config.js
echo.
pause

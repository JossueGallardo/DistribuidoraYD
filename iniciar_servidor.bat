@echo off
echo =================================================
echo     INICIANDO SERVIDOR DE PRODUCCION
echo =================================================
echo.

REM Verificar que la aplicación esté publicada
if not exist "publish\Reto3_YD.dll" (
    echo ERROR: La aplicacion no esta publicada
    echo Ejecuta primero: instalar_produccion.bat
    pause
    exit /b 1
)

echo Configurando variables de entorno...
set ASPNETCORE_ENVIRONMENT=Production
set ASPNETCORE_URLS=http://0.0.0.0:5210

echo.
echo Iniciando servidor en puerto 5210...
echo URL del backend: http://localhost:5210
echo.
echo IMPORTANTE: Actualiza la configuracion del frontend
echo Edita: Pagina Web Final\assets\js\config.js
echo Cambia TU_IP_SERVIDOR por la IP real del servidor
echo.
echo Presiona Ctrl+C para detener el servidor
echo.

cd publish
dotnet Reto3_YD.dll

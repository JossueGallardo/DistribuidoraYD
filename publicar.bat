@echo off
echo Publicando aplicacion ASP.NET Core...

REM Publicar la aplicación para producción
dotnet publish -c Release -o publish

echo Aplicacion publicada en la carpeta 'publish'
echo Para ejecutar en producción, use: dotnet Reto3_YD.dll
pause

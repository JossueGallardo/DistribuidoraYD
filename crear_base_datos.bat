@echo off
echo Iniciando script de creacion de base de datos...

REM Script para crear la base de datos en SQL Server
sqlcmd -S (localdb)\MSSQLLocalDB -Q "CREATE DATABASE Reto3YD_DB;"

echo Base de datos creada exitosamente.

REM Aplicar migraciones
echo Aplicando migraciones...
dotnet ef database update

echo Proceso completado.
pause

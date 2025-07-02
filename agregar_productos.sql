-- Script para agregar los productos que están en el HTML
-- Primero, limpiar productos existentes si los hay
DELETE FROM DetallesFactura;
DELETE FROM Facturas;
DELETE FROM Productos;

-- Agregar los productos que están en shop.html
INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen) VALUES 
('Quintal de Arroz Panda', 'Arroz de alta calidad marca Panda, ideal para toda la familia', 52.50, 20, 'assets/img/Panda1.jpeg'),
('Quintal de Azúcar San Carlos', 'Azúcar refinada San Carlos, dulzura natural para tu hogar', 52.50, 15, 'assets/img/Azucar1.jpg'),
('Aceite La Favorita 1 litro', 'Aceite vegetal La Favorita, perfecto para cocinar', 4.99, 30, 'assets/img/Aceite1.jpg'),
('Queso Mozarella 2000g', 'Queso mozarella fresco de 2kg, ideal para pizzas y cocina', 15.50, 10, 'assets/img/Queso Mozarella.jpg');

-- Agregar un cliente por defecto para las ventas
INSERT INTO Clientes (Nombre, Email) VALUES 
('Cliente General', 'cliente@distribuidorayd.com');

-- Verificar que se agregaron
SELECT * FROM Productos;
SELECT * FROM Clientes;

CREATE DATABASE LuchoSoftV1

USE LuchoSoftV1

CREATE TABLE Categoria_insumos (
    Id_categoria_insumos INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Nombre_categoria_insumos VARCHAR(45) NOT NULL,
    Estado_categoria_insumos TINYINT NOT NULL DEFAULT 1
);

CREATE TABLE Categoria_productos (
    Id_categoria_productos INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Nombre_categoria_productos VARCHAR(45) NOT NULL,
    Estado_categoria_productos TINYINT NOT NULL DEFAULT 1
);

CREATE TABLE Clientes (
    Id_cliente INT NOT NULL PRIMARY KEY,
    Nombre_cliente VARCHAR(30) NOT NULL,
    Telefono_cliente VARCHAR(13) NOT NULL,
    Direccion_cliente VARCHAR(30) NOT NULL,
    Cliente_frecuente TINYINT NOT NULL DEFAULT 0,
    Estado_cliente TINYINT NOT NULL DEFAULT 1
);

CREATE TABLE Proveedores (
    Id_proveedor INT NOT NULL PRIMARY KEY,
    Nombre_proveedor VARCHAR(30) NOT NULL,
    Telefono_proveedor VARCHAR(13) NOT NULL,
    Direccion_proveedor VARCHAR(30) NOT NULL,
    Estado_proveedor TINYINT NOT NULL DEFAULT 1
);

CREATE TABLE Roles (
    Id_rol INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Nombre_rol VARCHAR(45) NOT NULL,
    Descripcion_rol VARCHAR(150) NOT NULL,
    Estado_rol TINYINT NOT NULL DEFAULT 1
);

CREATE TABLE Compras (
    Id_compra INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Nombre_compra VARCHAR(45) NOT NULL,
    Fecha_compra DATE NOT NULL,
    Estado_compra INT NOT NULL,
    Total_compra FLOAT NOT NULL,
    Id_proveedor_compras INT,
    FOREIGN KEY (Id_proveedor_compras) REFERENCES Proveedores(Id_proveedor)
);

CREATE TABLE Insumos (
    Id_insumo INT NOT NULL PRIMARY KEY,
    Imagen_insumo VARBINARY(MAX) NULL,
    Nombre_insumo VARCHAR(45) NOT NULL,
    UnidadesDeMedida_insumo VARCHAR(45) NOT NULL,
    Stock_insumo FLOAT NOT NULL,
    Estado_insumo TINYINT NOT NULL DEFAULT 1,
    Id_categoria_insumo_insumos INT,
    FOREIGN KEY (Id_categoria_insumo_insumos) REFERENCES Categoria_insumos(Id_categoria_insumos)
);

CREATE TABLE Compras_insumos (
    Id_compras_insumos INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Cantidad_insumo_compras_insumos INT NOT NULL,
    Precio_insumo_compras_insumos FLOAT NOT NULL,
    Id_compra_compras_insumos INT,
    Id_insumo_compras_insumos INT,
    FOREIGN KEY (Id_compra_compras_insumos) REFERENCES Compras(Id_compra),
    FOREIGN KEY (Id_insumo_compras_insumos) REFERENCES Insumos(Id_insumo)
);

CREATE TABLE Empleados (
    Id_empleado INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Imagen_empleado VARBINARY(MAX) NULL,
    Nombre_empleado VARCHAR(30) NOT NULL,
    Telefono_empleado VARCHAR(13) NOT NULL,
    Direccion_empleado VARCHAR(30) NOT NULL,
    Estado_empleado TINYINT NOT NULL DEFAULT 1
);

CREATE TABLE Pedidos (
    Id_pedido INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Observaciones VARCHAR(150) NULL,
    Fecha_venta DATE NOT NULL,
    Fecha_pedido DATE NOT NULL,
    Estado_pedido INT NOT NULL,
    Total_venta FLOAT NOT NULL,
    Total_pedido FLOAT NOT NULL,
    Id_cliente_pedidos INT,
    Id_empleado_pedidos INT,
    FOREIGN KEY (Id_cliente_pedidos) REFERENCES Clientes(Id_cliente),
    FOREIGN KEY (Id_empleado_pedidos) REFERENCES Empleados(Id_empleado)
);

CREATE TABLE Productos (
    Id_producto INT NOT NULL PRIMARY KEY,
    Imagen_producto VARBINARY(MAX) NULL,
    Nombre_producto VARCHAR(45) NOT NULL,
    Descripcion_producto VARCHAR(150) NOT NULL,
    Estado_producto TINYINT NOT NULL DEFAULT 1,
    Precio_producto FLOAT NOT NULL,
    Id_categoria_producto_productos INT,
    FOREIGN KEY (Id_categoria_producto_productos) REFERENCES Categoria_productos(Id_categoria_productos)
);

CREATE TABLE Pedidos_productos (
    Id_pedidos_productos INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Fecha_pedido_producto DATE NOT NULL,
    Cantidad_producto FLOAT NOT NULL,
    Subtotal FLOAT NOT NULL,
    Id_producto_pedidos_productos INT,
    Id_pedido_pedidos_productos INT,
    FOREIGN KEY (Id_pedido_pedidos_productos) REFERENCES Pedidos(Id_pedido),
    FOREIGN KEY (Id_producto_pedidos_productos) REFERENCES Productos(Id_producto)
);

CREATE TABLE Ordenes_de_produccion (
    Id_orden_de_produccion INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Descripcion_orden VARCHAR(150) NOT NULL,
    Fecha_orden DATE NOT NULL,
    Id_empleado_ordenes_de_produccion INT,
    FOREIGN KEY (Id_empleado_ordenes_de_produccion) REFERENCES Empleados(Id_empleado)
);

CREATE TABLE Orden_insumos (
    Id_orden_insumos INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Descripcion_orden_insumos VARCHAR(150) NULL,
    Cantidad_insumo_orden_insumos FLOAT NOT NULL,
    Id_orden_de_produccion_orden_insumos INT,
    Id_insumo_orden_insumos INT,
    FOREIGN KEY (Id_orden_de_produccion_orden_insumos) REFERENCES Ordenes_de_produccion(Id_orden_de_produccion),
    FOREIGN KEY (Id_insumo_orden_insumos) REFERENCES Insumos(Id_insumo)
);

CREATE TABLE Usuarios (
    Id_usuario INT NOT NULL PRIMARY KEY,
    Nombre_usuario VARCHAR(30) NOT NULL,
    Email VARCHAR(45) NOT NULL,
    Contraseña VARCHAR(45) NOT NULL,
    Estado_usuario TINYINT NOT NULL DEFAULT 1,
    Id_rol_usuarios INT,
    FOREIGN KEY (Id_rol_usuarios) REFERENCES Roles(Id_rol)
);

CREATE TABLE Permisos (
    Id_permiso INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Nombre_permiso VARCHAR(45) NOT NULL,
    Estado_permiso TINYINT NOT NULL DEFAULT 1
);

CREATE TABLE Roles_permisos (
    Id_roles_permisos INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Fecha_roles_permisos DATE NOT NULL,
    Id_rol_roles_permisos INT,
    Id_permiso_roles_permisos INT,
    FOREIGN KEY (Id_rol_roles_permisos) REFERENCES Roles(Id_rol),
    FOREIGN KEY (Id_permiso_roles_permisos) REFERENCES Permisos(Id_permiso)
);

USE LuchoSoftV1 ;

-- Categoría de insumos
INSERT INTO Categoria_insumos (Nombre_categoria_insumos, Estado_categoria_insumos) VALUES
('Verduras', 1),
('Carne', 1),
('Mariscos', 1),
('Salsas', 1),
('Especias', 1);

-- Categoría de productos
INSERT INTO Categoria_productos (Nombre_categoria_productos, Estado_categoria_productos) VALUES
('Platos principales', 1),
('Entradas', 1),
('Bebidas', 1),
('Postres', 1),
('Menú especial', 1);

-- Clientes
INSERT INTO Clientes (Id_cliente, Nombre_cliente, Telefono_cliente, Direccion_cliente, Cliente_frecuente, Estado_cliente) VALUES
(1, 'Juan Pérez', '1234567890', 'Calle 123', 0, 1),
(2, 'Ana Gómez', '9876543210', 'Carrera 456', 1, 1),
(3, 'Carlos Rodríguez', '5555555555', 'Avenida 789', 0, 1),
(4, 'Laura Torres', '1111111111', 'Calle 456', 0, 1),
(5, 'Diego Ramírez', '9999999999', 'Carrera 789', 1, 1);

-- Proveedores
INSERT INTO Proveedores (Id_proveedor, Nombre_proveedor, Telefono_proveedor, Direccion_proveedor, Estado_proveedor) VALUES
(1, 'Proveedor A', '5555555555', 'Calle Proveedor 1', 1),
(2, 'Proveedor B', '7777777777', 'Carrera Proveedor 2', 1),
(3, 'Proveedor C', '8888888888', 'Avenida Proveedor 3', 1),
(4, 'Proveedor D', '9999999999', 'Calle Proveedor 4', 1),
(5, 'Proveedor E', '1111111111', 'Carrera Proveedor 5', 1);

-- Compras
INSERT INTO Compras (Nombre_compra, Fecha_compra, Estado_compra, Total_compra, Id_proveedor_compras) VALUES
('Compra de verduras', '2023-09-22', 1, 250.00, 1),
('Compra de carne', '2023-09-22', 1, 180.00, 2),
('Compra de mariscos', '2023-09-22', 1, 120.00, 3),
('Compra de salsas', '2023-09-22', 1, 75.00, 4),
('Compra de especias', '2023-09-22', 1, 50.00, 5);

-- Insumos
INSERT INTO Insumos (Id_insumo, Imagen_insumo, Nombre_insumo, UnidadesDeMedida_insumo, Stock_insumo, Estado_insumo, Id_categoria_insumo_insumos) VALUES
(1, NULL, 'Bambú', 'Kilogramos', 50.00, 1, 1),
(2, NULL, 'Pollo', 'Kilogramos', 35.50, 1, 2),
(3, NULL, 'Camarones', 'Kilogramos', 80.00, 1, 1),
(4, NULL, 'Salsa de soja', 'Litros', 25.25, 1, 4),
(5, NULL, 'Jengibre', 'Kilogramos', 60.75, 1, 5);

-- Compras de insumos
INSERT INTO Compras_insumos (Cantidad_insumo_compras_insumos, Precio_insumo_compras_insumos, Id_compra_compras_insumos, Id_insumo_compras_insumos) VALUES
(20, 3.00, 1, 1),
(15, 5.50, 2, 2),
(10, 12.00, 3, 3),
(8, 2.75, 4, 4),
(25, 1.50, 5, 5);

-- Empleados
INSERT INTO Empleados (Imagen_empleado, Nombre_empleado, Telefono_empleado, Direccion_empleado, Estado_empleado) VALUES
(NULL, 'Santiago', '555-123-4567', 'Calle Empleado 1', 1),
(NULL, 'Jennifer', '555-987-6543', 'Carrera Empleado 2', 1),
(NULL, 'Maria', '555-111-2222', 'Avenida Empleado 3', 1),
(NULL, 'Arley', '555-333-4444', 'Calle Empleado 4', 1),
(NULL, 'Juan', '555-555-5555', 'Carrera Empleado 5', 1);

-- Pedidos
INSERT INTO Pedidos (Observaciones, Fecha_venta, Fecha_pedido, Estado_pedido, Total_venta, Total_pedido, Id_cliente_pedidos, Id_empleado_pedidos) VALUES
('Sin picante', '2023-09-22', '2023-09-21', 1, 120.00, 115.00, 1, 1),
('Pedido vegetariano', '2023-09-22', '2023-09-20', 1, 75.00, 70.00, 2, 2),
('Sin gluten', '2023-09-22', '2023-09-19', 1, 90.00, 85.00, 3, 3),
('Pedido especial', '2023-09-22', '2023-09-18', 1, 150.00, 145.00, 4, 4),
('Pedido sorpresa', '2023-09-22', '2023-09-17', 1, 45.00, 40.00, 5, 5);

-- Productos
INSERT INTO Productos (Id_producto, Imagen_producto, Nombre_producto, Descripcion_producto, Estado_producto, Precio_producto, Id_categoria_producto_productos) VALUES
(1, NULL, 'Arroz frito', 'Arroz frito con verduras y pollo', 1, 12.00, 1),
(2, NULL, 'Rollos de primavera', 'Rollos de primavera con salsa agridulce', 1, 6.50, 2),
(3, NULL, 'Té de jazmín', 'Té de jazmín caliente', 1, 2.50, 3),
(4, NULL, 'Ternera con brócoli', 'Ternera salteada con brócoli', 1, 14.00, 1),
(5, NULL, 'Helado de lychee', 'Helado de lychee con almendras', 1, 5.00, 4);

-- Pedidos de productos
INSERT INTO Pedidos_productos (Fecha_pedido_producto, Cantidad_producto, Subtotal, Id_producto_pedidos_productos, Id_pedido_pedidos_productos) VALUES
('2023-09-21', 5.00, 60.00, 1, 1),
('2023-09-20', 3.00, 19.50, 2, 2),
('2023-09-19', 2.00, 5.00, 3, 3),
('2023-09-18', 4.00, 56.00, 4, 4),
('2023-09-17', 6.00, 30.00, 5, 5);

-- Órdenes de producción
INSERT INTO Ordenes_de_produccion (Descripcion_orden, Fecha_orden, Id_empleado_ordenes_de_produccion) VALUES
('Preparación de ingredientes', '2023-09-22', 1),
('Cocina de wok', '2023-09-22', 2),
('Preparación de bebidas', '2023-09-22', 3),
('Elaboración de postres', '2023-09-22', 4),
('Menú especial del día', '2023-09-22', 5);

-- Orden de insumos
INSERT INTO Orden_insumos (Descripcion_orden_insumos, Cantidad_insumo_orden_insumos, Id_orden_de_produccion_orden_insumos, Id_insumo_orden_insumos) VALUES
('Preparación de ingredientes', 30.00, 1, 1),
('Cocina de wok', 25.00, 2, 2),
('Preparación de bebidas', 20.00, 3, 3),
('Elaboración de postres', 15.00, 4, 4),
('Menú especial del día', 18.00, 5, 5);

-- Permisos
INSERT INTO Permisos (Nombre_permiso, Estado_permiso) VALUES
('Permiso de cocinero', 1),
('Permiso de camarero', 1),
('Permiso de cajero', 1),
('Permiso de gerente', 1),
('Permiso de repartidor', 1);

-- Roles
INSERT INTO Roles (Nombre_rol, Descripcion_rol, Estado_rol) VALUES
('Cocinero', 'Prepara deliciosos platos chinos', 1),
('Camarero', 'Atiende a los comensales', 1),
('Cajero', 'Maneja las transacciones', 1),
('Gerente', 'Supervisa las operaciones del restaurante', 1),
('Repartidor', 'Entrega pedidos a domicilio', 1);

-- Asignación de permisos a roles
INSERT INTO Roles_permisos (Fecha_roles_permisos, Id_rol_roles_permisos, Id_permiso_roles_permisos) VALUES
('2023-09-22', 1, 1),
('2023-09-22', 2, 2),
('2023-09-22', 3, 3),
('2023-09-22', 4, 4),
('2023-09-22', 5, 5);

-- Usuarios
INSERT INTO Usuarios (Id_usuario, Nombre_usuario, Email, Contraseña, Estado_usuario, Id_rol_usuarios) VALUES
(1, 'Santiago', 'cocinero1@example.com', 'contraseña1', 1, 1),
(2, 'Jennifer', 'camarero1@example.com', 'contraseña2', 1, 2),
(3, 'Maria', 'cajero1@example.com', 'contraseña3', 1, 3),
(4, 'Arley', 'gerente1@example.com', 'contraseña4', 1, 4),
(5, 'Juan', 'repartidor1@example.com', 'contraseña5', 1, 5);


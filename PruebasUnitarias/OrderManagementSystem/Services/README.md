# Sistema de Gestión de Órdenes de Compra

Este proyecto es una API REST desarrollada en **.NET Core** para administrar ordenes para el examen tecnico de estela.

### Crear un producto

POST http://localhost:5094/api/products
Content-Type: application/json

{
"name": "Laptop",
"price": 1200.00,
"stock": 10
}

### Crear una orden

POST http://localhost:5094/api/orders
Content-Type: application/json

{
"1": 2
}

### Consultar órdenes

GET http://localhost:5094/api/orders

### Verificar los Resultados

Crear Producto: Deberías recibir una respuesta con los detalles del producto creado.

Crear Orden: Deberías recibir una respuesta con los detalles de la orden creada.

Consultar Órdenes: Deberías recibir una lista de órdenes con sus detalles.

## 🚀 Tecnologías Utilizadas

- ASP.NET Core
- Visual Studio Code

## 📌 Instalación

1. Clona el repositorio:
   ```sh
   git clone https://github.com/cvelasquez/examenEstela
   cd tu-repo
   ```

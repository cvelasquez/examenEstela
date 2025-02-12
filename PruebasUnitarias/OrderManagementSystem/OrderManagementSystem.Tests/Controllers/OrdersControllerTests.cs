using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagementSystem.Controllers;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services;
using Xunit;

namespace OrderManagementSystem.Tests.Controllers
{
    public class OrdersControllerTests
    {
        [Fact]
        public void CreateOrder_ReturnsOkResult_WhenOrderIsValid()
        {
            // Arrange
            var mockProductService = new Mock<ProductService>();
            var mockOrderService = new Mock<OrderService>(mockProductService.Object);

            // Configurar el mock para simular el comportamiento de OrderService
            mockOrderService
                .Setup(service => service.CreateOrder(It.IsAny<Dictionary<int, int>>()))
                .Returns(new Order { Id = 1 });

            var controller = new OrdersController(mockOrderService.Object);

            var productQuantities = new Dictionary<int, int>
            {
                { 1, 2 } // Producto ID 1, cantidad 2
            };

            // Act
            var result = controller.CreateOrder(productQuantities);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var order = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(1, order.Id);
        }

        [Fact]
        public void CreateOrder_ReturnsBadRequest_WhenProductDoesNotExist()
        {
            // Arrange
            var mockProductService = new Mock<ProductService>();
            var mockOrderService = new Mock<OrderService>(mockProductService.Object);

            // Configurar el mock para simular una excepción
            mockOrderService
                .Setup(service => service.CreateOrder(It.IsAny<Dictionary<int, int>>()))
                .Throws(new ArgumentException("Producto no encontrado."));

            var controller = new OrdersController(mockOrderService.Object);

            var productQuantities = new Dictionary<int, int>
            {
                { 999, 2 } // Producto ID 999 (no existe)
            };

            // Act
            var result = controller.CreateOrder(productQuantities);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Producto no encontrado.", badRequestResult.Value);
        }

        [Fact]
        public void CreateOrder_ReturnsBadRequest_WhenStockIsInsufficient()
        {
            // Arrange
            var mockProductService = new Mock<ProductService>();
            var mockOrderService = new Mock<OrderService>(mockProductService.Object);

            // Configurar el mock para simular una excepción
            mockOrderService
                .Setup(service => service.CreateOrder(It.IsAny<Dictionary<int, int>>()))
                .Throws(new InvalidOperationException("No hay suficiente stock."));

            var controller = new OrdersController(mockOrderService.Object);

            var productQuantities = new Dictionary<int, int>
            {
                { 1, 100 } // Producto ID 1, cantidad 100 (stock insuficiente)
            };

            // Act
            var result = controller.CreateOrder(productQuantities);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No hay suficiente stock.", badRequestResult.Value);
        }
    }
}
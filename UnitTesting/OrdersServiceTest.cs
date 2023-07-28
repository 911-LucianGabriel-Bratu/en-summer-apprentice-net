using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Models;
using TicketManagementSystem.Repositories;
using TicketManagementSystem.Services;

namespace UnitTesting
{
    [TestClass]
    public class OrdersServiceTest
    {
        Mock<IOrdersRepository> _ordersRepositoryMoq;
        Mock<IMapper> _mapperMoq;
        List<Order> _ordersListMoq;
        List<OrdersDTO> _ordersDTOListMoq;

        [TestInitialize]
        public void SetupMoqData()
        {
            _ordersRepositoryMoq = new Mock<IOrdersRepository>();
            _mapperMoq = new Mock<IMapper>();
            _ordersListMoq = new List<Order>
            {
                new Order {
                    OrderId = 1,
                    CustomerId = 1,
                    TicketCategoryId = 1,
                    OrderedAt = DateTime.Now,
                    NumberOfTickets = 1,
                    TotalPrice = 1,
                    Customer = new Customer{ CustomerId = 1, CustomerName = "test customer", Email = "test email" },
                    TicketCategory = new TicketCategory { TicketCategoryId = 1, Description = "test description", Price = 1 }
                }
            };
            _ordersDTOListMoq = new List<OrdersDTO>
            {
                new OrdersDTO
                {
                    OrderId = 1,
                    CustomerName = "test customer",
                    TicketCategoryDescription = "test description",
                    OrderedAt = DateTime.Now,
                    NumberOfTickets = 1,
                    TotalPrice = 1
                }
            };
        }

        [TestMethod]
        public async Task GetAllOrdersReturnOrdersList()
        {
            //Arrange
            List<Order> moqOrders = _ordersListMoq;
            Task<List<Order>> moqTask = Task.Run(() => moqOrders);
            _ordersRepositoryMoq.Setup(moq => moq.GetOrders()).Returns(moqTask);
            _mapperMoq.Setup(moq => moq.Map<List<OrdersDTO>>(It.IsAny<List<Order>>())).Returns(_ordersDTOListMoq);

            var service = new OrdersService(_ordersRepositoryMoq.Object, _mapperMoq.Object);

            //Act
            var orders = await service.GetOrders();

            //Assert
            Assert.IsNotNull(orders);
            Assert.AreEqual(1, orders.Count);
        }

        [TestMethod]
        public async Task GetOrderByIdEntryFound()
        {
            //Arrange
            _ordersRepositoryMoq.Setup(moq => moq.GetOrderById(It.IsAny<int>())).Returns(Task.Run(() => _ordersListMoq.First()));
            _mapperMoq.Setup(moq => moq.Map<OrdersDTO>(It.IsAny<Order>())).Returns(_ordersDTOListMoq.First());

            //Act
            var service = new OrdersService(_ordersRepositoryMoq.Object, _mapperMoq.Object);

            //Assert
            Assert.IsNotNull(await service.GetOrderById(1));
        }

        [TestMethod]
        public async Task GetOrderByIdNoEntryFound()
        {
            //Arrange
            _ordersRepositoryMoq.Setup(moq => moq.GetOrderById(It.IsAny<int>())).Returns(Task.Run(() => _ordersListMoq.First()));
            _mapperMoq.Setup(moq => moq.Map<OrdersDTO>(It.IsAny<Order>())).Returns((OrdersDTO)null);

            //Act
            var service = new OrdersService(_ordersRepositoryMoq.Object, _mapperMoq.Object);

            //Assert
            Assert.IsNull(await service.GetOrderById(1));
        }

        [TestMethod]
        public async Task DeleteOrderByIdEntryFound()
        {
            //Arrange
            _ordersRepositoryMoq.Setup(moq => moq.GetOrderById(It.IsAny<int>())).Returns(Task.Run(() => _ordersListMoq.First()));
            OrdersDTO orderDTO = _mapperMoq.Object.Map<OrdersDTO>(_ordersListMoq.First());

            //Act
            var service = new OrdersService(_ordersRepositoryMoq.Object, _mapperMoq.Object);
            var response = await service.RemoveOrder(1);

            //Assert
            Assert.AreSame(orderDTO, response);
        }

        [TestMethod]
        public async Task DeleteOrderByIdNoEntryFound()
        {
            //Arrange
            _ordersRepositoryMoq.Setup(moq => moq.GetOrderById(It.IsAny<int>())).Returns(Task.Run(() => _ordersListMoq.First()));
            _mapperMoq.Setup(moq => moq.Map<OrdersDTO>(It.IsAny<Order>())).Returns((OrdersDTO)null);

            //Act
            var service = new OrdersService(_ordersRepositoryMoq.Object, _mapperMoq.Object);
            var response = await service.RemoveOrder(1);

            //Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public async Task UpdateOrderByIdEntryFound()
        {
            //Arrange
            OrdersUpdateDTO mockOrder = new OrdersUpdateDTO
            {
                OrderedAt = DateTime.Now,
                NumberOfTickets = 2
            };
            Order prevUpdateOrder = _ordersListMoq.First();
            _ordersRepositoryMoq.Setup(moq => moq.GetOrderById(It.IsAny<int>())).Returns(Task.Run(() => _ordersListMoq.First()));
            _ordersRepositoryMoq.Setup(moq => moq.UpdateOrder(It.IsAny<long>(), It.IsAny<OrdersUpdateDTO>())).Returns(Task.Run(() => mockOrder));

            //Act
            var service = new OrdersService(_ordersRepositoryMoq.Object, _mapperMoq.Object);
            var response = await service.UpdateOrder(1, mockOrder);

            //Assert
            Assert.AreNotSame(_ordersRepositoryMoq.Object.GetOrderById(1), prevUpdateOrder);
            Assert.IsNotNull(response);
            Assert.AreNotEqual(prevUpdateOrder.NumberOfTickets, response.NumberOfTickets);
        }

        [TestMethod]
        public async Task UpdateOrderByIdNoEntryFound()
        {
            //Arrange
            OrdersUpdateDTO mockOrder = new OrdersUpdateDTO
            {
                OrderedAt = DateTime.Now,
                NumberOfTickets = 2
            };
            Order prevUpdateOrder = _ordersListMoq.First();
            _ordersRepositoryMoq.Setup(moq => moq.GetOrderById(It.IsAny<int>())).Returns(Task.Run(() => _ordersListMoq.First()));
            _ordersRepositoryMoq.Setup(moq => moq.UpdateOrder(It.IsAny<long>(), It.IsAny<OrdersUpdateDTO>())).Returns(Task.Run(() => (OrdersUpdateDTO) null));

            //Act
            var service = new OrdersService(_ordersRepositoryMoq.Object, _mapperMoq.Object);
            var response = await service.UpdateOrder(2, mockOrder);

            //Assert
            Assert.IsNull(response);
        }
    }
}

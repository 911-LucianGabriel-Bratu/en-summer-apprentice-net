using AutoMapper;
using Castle.Core.Logging;
using Moq;
using TicketManagementSystem.Controllers;
using TicketManagementSystem.Middleware;
using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Repositories;
using TicketManagementSystem.Services;

namespace UnitTesting
{
    [TestClass]
    public class EventsServiceTest
    {
        Mock<IEventRepository> _eventRepositoryMoq;
        Mock<IMapper> _mapperMoq;
        List<Event> _eventListMoq;
        List<EventDTO> _eventDTOListMoq;

        [TestInitialize]
        public void SetupMoqData()
        {
            _eventRepositoryMoq = new Mock<IEventRepository>();
            _mapperMoq = new Mock<IMapper>();
            _eventListMoq = new List<Event>
            {
                new Event
                {
                    EventId = 1,
                    EventName = "Test event",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    EventDescription = "Test event description",
                    Venue = new Venue{VenueId = 1, Capacity = 10, Location = "Mock location", Type = "Mock venue type"},
                    VenueId = 1,
                    EventType = new EventType { EventTypeId = 1, EventTypeName = "Mock event type name"},
                    EventTypeId = 1                    
                }
            };
            _eventDTOListMoq = new List<EventDTO>
            {
                new EventDTO
                {
                    EventId = 1,
                    EventName = "Test event",
                    EventDescription = "Test event description",
                    EventType = "Mock event type name",
                    Venue = "Mock location"
                }
            };
        }

        [TestMethod]
        public async Task GetAllEventsReturnEventList()
        {
            //Arrange
            List<Event> moqEvents = _eventListMoq;
            Task<List<Event>> moqTask = Task.Run(() => moqEvents);
            _eventRepositoryMoq.Setup(moq => moq.GetEvents()).Returns(moqTask);
            _mapperMoq.Setup(moq => moq.Map<List<EventDTO>>(It.IsAny<List<Event>>())).Returns(_eventDTOListMoq);

            var service = new EventService(_eventRepositoryMoq.Object, _mapperMoq.Object);

            //Act
            var events = await service.GetEvents();

            //Assert
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count);
        }

        [TestMethod]
        public async Task GetEventByIdNoEntryFound()
        {
            //Arrange
            _eventRepositoryMoq.Setup(moq => moq.GetEventById(It.IsAny<int>())).Returns(Task.Run(() => _eventListMoq.First()));
            _mapperMoq.Setup(moq => moq.Map<EventDTO>(It.IsAny<Event>())).Returns((EventDTO) null);

            //Act
            var service = new EventService(_eventRepositoryMoq.Object, _mapperMoq.Object);

            //Assert
            Assert.IsNull(await service.GetEventById(1));
        }

        [TestMethod]
        public async Task GetEventByIdEntryFound()
        {
            //Arrange
            _eventRepositoryMoq.Setup(moq => moq.GetEventById(It.IsAny<int>())).Returns(Task.Run(() => _eventListMoq.First()));
            _mapperMoq.Setup(moq => moq.Map<EventDTO>(It.IsAny<Event>())).Returns(_eventDTOListMoq.First());

            //Act
            var service = new EventService(_eventRepositoryMoq.Object, _mapperMoq.Object);

            //Assert
            Assert.IsNotNull(await service.GetEventById(1));
        }

        [TestMethod]
        public async Task DeleteEventByIdEntryFound()
        {
            //Arrange
            _eventRepositoryMoq.Setup(moq => moq.GetEventById(It.IsAny<int>())).Returns(Task.Run(() => _eventListMoq.First()));
            EventDTO eventDTO = _mapperMoq.Object.Map<EventDTO>(_eventListMoq.First());

            //Act
            var service = new EventService(_eventRepositoryMoq.Object, _mapperMoq.Object);
            var response = await service.RemoveEvent(1);

            //Assert
            Assert.AreSame(eventDTO, response);
        }

        [TestMethod]
        public async Task DeleteEventByIdEntryNotFound()
        {
            //Arrange
            _eventRepositoryMoq.Setup(moq => moq.GetEventById(It.IsAny<int>())).Returns(Task.Run(() => _eventListMoq.First()));
            _mapperMoq.Setup(moq => moq.Map<EventDTO>(It.IsAny<Event>())).Returns((EventDTO)null);

            //Act
            var service = new EventService(_eventRepositoryMoq.Object, _mapperMoq.Object);
            var response = await service.RemoveEvent(1);

            //Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public async Task UpdateEventByIdEntryFound()
        {
            //Arrange
            EventUpdateDTO mockEvent = new EventUpdateDTO
            {
                EventName = "Test event update",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                EventDescription = "Test event update description"
            };
            Event prevUpdateEvent = _eventListMoq.First();
            _eventRepositoryMoq.Setup(moq => moq.GetEventById(It.IsAny<int>())).Returns(Task.Run(() => _eventListMoq.First()));
            _eventRepositoryMoq.Setup(moq => moq.UpdateEvent(It.IsAny<long>(), It.IsAny<EventUpdateDTO>())).Returns(Task.Run(() => mockEvent));

            //Act
            var service = new EventService(_eventRepositoryMoq.Object, _mapperMoq.Object);
            var response = await service.UpdateEvent(1, mockEvent);

            //Assert
            Assert.AreNotSame(_eventRepositoryMoq.Object.GetEventById(1), prevUpdateEvent);
            Assert.IsNotNull(response);
            Assert.AreNotEqual(prevUpdateEvent.EventName, response.EventName);
            Assert.AreNotEqual(prevUpdateEvent.EventDescription, response.EventDescription);
        }

        [TestMethod]
        public async Task UpdateEventByIdEntryNotFound()
        {
            //Arrange
            EventUpdateDTO mockEvent = new EventUpdateDTO
            {
                EventName = "Test event update",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                EventDescription = "Test event update description"
            };
            Event prevUpdateEvent = _eventListMoq.First();
            _eventRepositoryMoq.Setup(moq => moq.GetEventById(It.IsAny<int>())).Returns(Task.Run(() => _eventListMoq.First()));
            _eventRepositoryMoq.Setup(moq => moq.UpdateEvent(It.IsAny<long>(), It.IsAny<EventUpdateDTO>())).Returns(Task.Run(() => (EventUpdateDTO) null));

            //Act
            var service = new EventService(_eventRepositoryMoq.Object, _mapperMoq.Object);
            var response = await service.UpdateEvent(2, mockEvent);

            //Assert
            Assert.IsNull(response);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViagogoTickets.Controllers;
using ViagogoTickets.Models;
using ViagogoTickets.Services;
using Xunit;

namespace ViagogoTickets.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Events_ReturnsAViewResult_WithAnEventsViewModel()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockService = new Mock<IEventService>();
            mockConfiguration.Setup(config => config["CATEGORY_ID"]).Returns("100");
            mockService.Setup(service => service.GetAllEvents(100)).Returns(Task.FromResult(GetTestEventList()));
            var controller = new HomeController(mockConfiguration.Object, mockService.Object);

            var result = await controller.Events();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EventsViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.EventList.Count());
        }

        private IEnumerable<EventModel> GetTestEventList()
        {
            var testList = new List<EventModel>
            {
                new EventModel()
                {
                    Id = 1,
                    Name = "Test1",
                },
                new EventModel()
                {
                    Id = 2,
                    Name = "Test2",
                }
            };
            return testList;
        }
    }
}


using FlightService.Controllers;
using FlightService.Domain;
using FlightService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlightServiceTest
{

    public class Generators {

        private Random random = new Random();

        private static int[] integerCornerCases = { 0, 1, -1, int.MaxValue, int.MinValue };
        private int intCalls = 0;
        public int integer() {
            if (intCalls < integerCornerCases.Length)
                return integerCornerCases[intCalls++];
            else
                return random.Next(int.MinValue, int.MaxValue);
        }
    }

    public class FlightTest
    {
        [Fact]
        public void constructorSetsId()
        {
            Generators gen = new Generators();
            for (int i = 0; i < 1000; i++) {
                int id = gen.integer();

                Flight flight = new Flight(id);
                Assert.Equal(id, flight.getId());

            }
        }

        [Fact]
        public void serviceGetReturnsFlight()
        {
            IFlightService flightService = new FlightService.Services.FlightService(Mock.Of<ILogger<FlightService.Services.FlightService>>());

            Generators gen = new Generators();
            for (int i = 0; i < 1000; i++)
            {
                int id = gen.integer();

                Flight flight = flightService.getFlight(id);
                Assert.Equal(id, flight.getId());
            }
        }

        [Fact]
        public void controllerGetReturnsFlight()
        {
            var mockService = new Mock<IFlightService>();
            mockService.Setup(service => 
                service.getFlight(It.IsAny<int>())
                )
                .Returns((int id) => 
                new Flight(id)
                );
            FlightController controller = new FlightController(mockService.Object, Mock.Of<ILogger<FlightController>>());

            Generators gen = new Generators();
            for (int i = 0; i < 1000; i++)
            {
                int id = gen.integer();

                ActionResult<Flight> result = controller.Get(id);
                Assert.Equal(id, result.Value?.getId());
            }
        }
    }
}

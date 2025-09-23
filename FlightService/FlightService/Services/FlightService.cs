using FlightService.Domain;

namespace FlightService.Services
{
    public class FlightService(ILogger<FlightService> logger) : IFlightService
    {
        Flight IFlightService.getFlight(int id)
        {
            logger.LogInformation("Creating flight with id " + id);  //TODO make this structured (json)
            return new Flight(id);
        }
    }
}

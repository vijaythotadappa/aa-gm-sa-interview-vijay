using FlightService.Domain;
using FlightService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FlightService.Controllers
{
    [Route("api/flight")]
    [ApiController]
    public class FlightController(IFlightService flightService, ILogger<FlightController> logger) : ControllerBase
    {
        [HttpGet("{id}")]
        public Flight Get(int id)
        {
            logger.LogInformation("get flight " + id);  //TODO make this structured (json)
            return flightService.getFlight(id);
        }
    }
}

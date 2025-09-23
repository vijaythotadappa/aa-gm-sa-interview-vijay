using FlightService.Domain;

namespace FlightService.Services
{
    public interface IFlightService
    {
        public Flight getFlight(int id);
    }
}

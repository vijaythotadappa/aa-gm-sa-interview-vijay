namespace FlightService.Domain
{
    public class Flight
    {
        private int id;

        public Flight(int id) {
            this.id = id;
        }

        public int getId() { return id; }
    }
}

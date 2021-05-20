using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class FlightList 
    {
        public List<Flight> Flights = new List<Flight>();

        public FlightList()
        {
            Flights.Add(new Flight("Boeing 737", "B737Cal", "California", 100));
            Flights.Add(new Flight("Boeing 737", "B737Lon", "London", 100));
            Flights.Add(new Flight("Airbus A320", "A320Tok", "Tokyo", 100));
            Flights.Add(new Flight("Airbus A320", "A320Sid", "Sidney", 100));
            Flights.Add(new Flight("Airbus A320", "A320Lon", "London", 100));



        }
    }
}

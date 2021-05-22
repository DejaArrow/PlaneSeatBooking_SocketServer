using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Server
{
    public static class FlightList
    {
        public static List<Flight> Flights = new List<Flight>();

        static FlightList()
        //Populates FlightList with Flight data.
        {
            Flights.Add(new Flight("Boeing 737", "B737Cal", "California", 108));
            Flights.Add(new Flight("Boeing 737", "B737Lon", "London", 108));
            Flights.Add(new Flight("Airbus A320", "A320Tok", "Tokyo", 108));
            Flights.Add(new Flight("Airbus A320", "A320Sid", "Sidney", 108));
            Flights.Add(new Flight("Airbus A320", "A320Lon", "London", 108));
        }
    }
}

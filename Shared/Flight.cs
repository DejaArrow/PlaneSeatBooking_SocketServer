using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Flight
        //Plane Factory
    {
        public List<Seat> Seats = new List<Seat>();
        public string Type { get; }
        public string FlightID { get; }
        public string Destination { get; }
        public int NoOfSeats { get; }

        public Flight(string type, string flightID, string destination, int noOfSeats)
        {
            this.Type = type;
            this.FlightID = flightID;
            this.Destination = destination;
            this.NoOfSeats = noOfSeats;
            for (int i = 0; i < noOfSeats; i++)
                {
                Seats.Add(new Seat((i + 1).ToString()));
                }
        }

        public bool BookSeat(string seatID)
        //Switches Seat state to Occupied when booked.
        {
            Seat SeatToBook = Seats.Find(x => x.SeatID == seatID);
            if (SeatToBook.Occupation == false)
            {
                SeatToBook.Occupation = true;
                return true;
            }
            else
            {
                return false;
            }
        }


        public string GetAvailableSeats()
        //Lists all available seats for the selected flight.
        {
            string availableSeats = "";
            foreach (Seat seat in Seats)
            {
                if (!seat.Occupation)
                {
                    availableSeats += $"{seat.SeatID} is available\n";
                }
            }
            return availableSeats;
        }


        public string GetSeatingPlan()
        //Iterator Pattern
        //Displays Seating plan for flight, represented by 'H' for available seats, occupied seats represented by 'O'
        {
            int RowCounter = 0;
            string SeatingPlan = "";

            for (int i = 0; i < NoOfSeats; i++)
            {
                if (Seats[i].Occupation)
                {
                    SeatingPlan += "O";

                }
                else
                {
                    SeatingPlan += "H";
                }

                RowCounter++;

                if (RowCounter == 3 || RowCounter == 6)
                {
                    SeatingPlan += " ";
                }
                if (RowCounter == 9)
                {
                    SeatingPlan += "\n";
                    RowCounter = 0;
                }
            }

            return SeatingPlan;
        }
    }
}

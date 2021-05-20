using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Flight
    {
        public List<Seat> Seats = new List<Seat>();
        public string Type { get; }
        public string FlightID { get; }
        public string Destination { get; }
        public int NoOfSeats { get; }

        // public override int GetHashCode() => (FlightID).GetHashCode();
        //This is also incorrect? As we're hashing the seat number into the different flight tables?

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
        {
            Seat SeatToBook = Seats.Find(x => x.SeatID == seatID);
            if(SeatToBook.Occupation == false)
            {
                SeatToBook.Occupation = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayAvailableSeats()
        {
            foreach(Seat seat in Seats)
            {
                if(!seat.Occupation)
                {
                    Console.WriteLine($"{seat.SeatID} is available.");
                }
            }
        }

        public void DisplaySeatList()
        {
            int RowCounter = 0;

            for (int i = 0; i < NoOfSeats; i++)
            {
                if (Seats[i].Occupation)
                {
                    Console.Write("O", Console.ForegroundColor = ConsoleColor.Red);
                    
                }
                else
                {
                    Console.Write("H", Console.ForegroundColor = ConsoleColor.Gray);
                }

                RowCounter++;

                if (RowCounter == 3 || RowCounter ==6)
                {
                    Console.Write(" ");
                }
                if(RowCounter == 9)
                {
                    Console.Write("\n");
                    RowCounter = 0;
                }
            }
        }
    }
}

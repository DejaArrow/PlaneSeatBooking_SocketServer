using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared;

namespace Client
{
    class Program
    {
        // Main Method 
        static void Main(string[] args)
        {
          
            
            Flight flight = new Flight("Boeing 737", "B737Cal", "California", 100);
            MakeBooking(flight);
            MakeBooking(flight);
            MakeBooking(flight);

            flight.DisplaySeatList();
            Console.WriteLine("\n");
            flight.DisplayAvailableSeats();
            Console.ReadKey();
            Thread.Sleep(2000);
            new DataClient().Run();


        }
        public static void MakeBooking(Flight flight)
        {
            Console.WriteLine("Enter Seat Number: ");
            string SeatNo = Console.ReadLine();
            bool Successful = flight.BookSeat(SeatNo); //move to server
            //Send SeatID to server, await response 
            if(Successful)
            {
                Console.WriteLine("Seat Booked Successfully.");
            }
            else
            {
                Console.WriteLine("Seat Unavailable");
            }
        }
    }

    
    
}

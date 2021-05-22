using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Shared;

namespace Client
{
    public class Program
    {
        private static bool done = false;
        static List<Flight> Flights = new List<Flight>();

        // Main Method 
        public static void Main(string[] args)
        {
                                          
            do
            {
                //Facade Pattern
                //Displays Menu for user.
                Console.Clear();
                Console.WriteLine("Menu");

                Console.WriteLine("1) Make a Booking");
                Console.WriteLine("2) View Available Seats");
                Console.WriteLine("3) View Seating Plan");

                ConsoleKeyInfo MenuOption = Console.ReadKey(true);
                DisplayFlightList();

                if (MenuOption.Key == ConsoleKey.D1)
                {
                    MakeBooking(SelectFlight());
                }
                else if (MenuOption.Key == ConsoleKey.D2)
                {
                    ShowAvailableSeats(SelectFlight());
                }
                else if (MenuOption.Key == ConsoleKey.D3)
                {
                    DisplaySeatPlan(SelectFlight());
                }
                else
                {
                    Console.WriteLine("Invalid Option");
                }


                Console.WriteLine("\nReturn to main menu? Y/N ");
                ConsoleKeyInfo Selection = Console.ReadKey(true);

                if (Selection.Key == ConsoleKey.N)
                {
                    done = true;
                    Environment.Exit(0);
                    //When done, user can exit.
                }


            } while (!done);
                                              
            Console.ReadKey();
            Thread.Sleep(2000);
           
            }

        public static void DisplayFlightList()
        {
            //Asks Server for Flight List Data and displays it.
            List<string> FlightIDList = new DataClient().RequestFlightIDs();

            for(int i = 0; i < FlightIDList.Count; i++)
            {
                Console.WriteLine($"{i}) {FlightIDList[i]}");
            }
        }

        public static int SelectFlight()
        {
            //Takes user input and sends to Server to save as selected Flight.
            Console.WriteLine($"Select a Flight: 0-{new DataClient().Request("NoOfFlights")}");
            int FlightSelected = Convert.ToInt32(Console.ReadLine());
            return FlightSelected;
        }

        public static void MakeBooking(int flightID)
        {
            //Sends seat number to the Server to be listed under the selected flight.

            Console.WriteLine("\nEnter Seat Number: ");

            int SeatNo = Convert.ToInt32(Console.ReadLine());

            string BookingStatus = new DataClient().Request($"BookASeat,{flightID},{SeatNo}");
            Console.WriteLine($"{BookingStatus}");

                          
           
        }

        public static void DisplaySeatPlan(int flightID)
        {
            //Asks for the seatplan from the Server and displays it.
            string SeatPlan = new DataClient().Request($"SeatPlan,{flightID}");
            Console.WriteLine($"{SeatPlan}");

        }

        public static void ShowAvailableSeats(int flightID)
        {
            //Shows available seats from the selected flight.
            string AvailableSeats = new DataClient().Request($"AvailableSeats,{flightID}");
            Console.WriteLine($"{AvailableSeats}");
        }
    }

    
    
}

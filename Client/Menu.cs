using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Menu
    {
        public int flightSelect;
        public string flightID;

        public void DisplayMenu()
        {
            
        
            Console.WriteLine("-----Flight Seat Booking System-----");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Choose Flight: ");
            Console.WriteLine("1) B737Cal");
            Console.WriteLine("2) B737Lon");
            Console.WriteLine("3) A320Tok");
            Console.WriteLine("4) A320Sid");
            Console.WriteLine("5) A320Lon");
            flightSelect = int.Parse(Console.ReadLine());

            try
            {
                switch (flightSelect)
                {
                    case 1:
                        flightID = "B737Cal";
                        
                        
                        break;
                    case 2:
                        flightID = "B737Lon";

                        break;
                    case 3:
                        flightID = "A320Tok";

                        break;
                    case 4:
                        flightID = "A320Sid";

                        break;
                    case 5:
                        flightID = "A320Lon";

                        break;

                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Invalid Option");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Seat
        //Singleton 
    {
        public string SeatID { get; set; }
        public bool Occupation { get; set; }

        public Seat(string seatID)
        {
            this.SeatID = seatID;
            this.Occupation = false;
        }
    }
}

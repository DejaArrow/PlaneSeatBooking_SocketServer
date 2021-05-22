using System;
using static System.Console;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Shared;
using System.Collections.Generic;

namespace Server
{
    

    class DataServer
    {
        /// <summary>
        /// Run a DataServer instance.
        /// </summary>
        public void Run()
        {
            IPHostEntry ipHostDetails = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddressDetails = ipHostDetails.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddressDetails, 4242);

            Socket listenerSocket = new Socket(ipAddressDetails.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenerSocket.Bind(localEndPoint);
                listenerSocket.Listen(10);

                while (true)
                {
                    WriteLine("Listening for data ");
                    Socket clientSocket = listenerSocket.Accept();

                    // Data buffer 
                    byte[] bytes = new Byte[4096];
                    string data = null;
                    // Get the data from the server
                    while (true)
                    {
                        int numberOfBytes = clientSocket.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, numberOfBytes);
                        if (data.IndexOf("<EOF>", StringComparison.Ordinal) > -1)
                            break;
                    }
                    string serialisedXml = data.Substring(0,data.Length - 5);
                    DataItem dataItem = DataItemSerialisation.GetDataItem(serialisedXml);
                    WriteLine("Text received -> {0} ", dataItem.package);
                    string[] packageItems = dataItem.package.Split(',');

                    if (packageItems[0] == "RequestFlightList")
                     //Chain of Responsibility 
                    {
                       
                        List<string> FlightIDList = new List<string>();

                        foreach (Flight flight in FlightList.Flights)
                        {
                            FlightIDList.Add(flight.FlightID);
                        }

                        FlightItemList response = new FlightItemList(FlightIDList);
                        string serialisedItem = FlightItemListSerialisation.GetSerialisedDataItem(response);
                        byte[] message = Encoding.ASCII.GetBytes(serialisedItem);

                        clientSocket.Send(message);
                        //Recieved and responds to quest for Flight list. 
                    }
                    else if (packageItems[0] == "NoOfFlights")
                    {
                        DataItem response = new DataItem((FlightList.Flights.Count -1).ToString());
                        string serialisedItem = DataItemSerialisation.GetSerialisedDataItem(response);                      
                                                                     
                        byte[] message = Encoding.ASCII.GetBytes(serialisedItem);

                        clientSocket.Send(message);
                        //Returns list of Flights
                    }
                    else if (packageItems[0] == "BookASeat")
                    {
                        
                        bool Successful = FlightList.Flights[Convert.ToInt32(packageItems[1])].BookSeat(packageItems[2]);
                        DataItem response = new DataItem(" ");
                        if (Successful)
                        {
                             response = new DataItem("Seat Booked Successfully");
                            
                        }
                        else
                        {
                             response = new DataItem("Seat Unavailable");
                        }
                        //Prevents seat being booked if already occupied.
                        
                        
                        string serialisedItem = DataItemSerialisation.GetSerialisedDataItem(response);

                        byte[] message = Encoding.ASCII.GetBytes(serialisedItem);

                        clientSocket.Send(message);
                    }
                    else if (packageItems[0] == "SeatPlan")
                    {
                        DataItem response = new DataItem(FlightList.Flights[Convert.ToInt32(packageItems[1])].GetSeatingPlan());
                        string serialisedItem = DataItemSerialisation.GetSerialisedDataItem(response);

                        byte[] message = Encoding.ASCII.GetBytes(serialisedItem);

                        clientSocket.Send(message);
                        //Sends seating plan to client
                    }
                    else if (packageItems[0] == "AvailableSeats")
                    {
                        DataItem response = new DataItem(FlightList.Flights[Convert.ToInt32(packageItems[1])].GetAvailableSeats());
                        string serialisedItem = DataItemSerialisation.GetSerialisedDataItem(response);

                        byte[] message = Encoding.ASCII.GetBytes(serialisedItem);

                        clientSocket.Send(message);
                        //Sends available seat list to Client.
                    }




                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }
            catch (Exception e)
            {
                WriteLine(e.ToString());
            }
        }
    }
}

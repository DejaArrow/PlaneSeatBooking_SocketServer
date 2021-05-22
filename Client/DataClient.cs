using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Shared;

namespace Client
{
    public class DataClient
    {    /// <summary>
         /// Runs DataClient instances that requests data to the server.
         /// </summary>
         /// 
        
        public string Request(string message)
        {
            try
            {
                IPHostEntry ipHostDetails = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddressDetails = ipHostDetails.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddressDetails, 4242);

                Socket sender = new Socket(ipAddressDetails.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    DataItem request = new DataItem(message);
                    string serialisedItem = DataItemSerialisation.GetSerialisedDataItem(request);
                    sender.Connect(localEndPoint);
                   
                    // Send data request to server
                    byte[] messageToSend = Encoding.ASCII.GetBytes(serialisedItem + "<EOF>");
                    int byteSent = sender.Send(messageToSend);

                    // Data buffer 
                    byte[] messageReceived = new byte[4096];

                    // Recieve answer from server
                    int byteRecv = sender.Receive(messageReceived);
                    string response = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);



                    DataItem dataItem = DataItemSerialisation.GetDataItem(response);
                    return dataItem.package;
                    

                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    return "";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }

       

        public List<string> RequestFlightIDs()
        {
            try
            {
                IPHostEntry ipHostDetails = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddressDetails = ipHostDetails.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddressDetails, 4242);

                Socket sender = new Socket(ipAddressDetails.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    DataItem request = new DataItem("RequestFlightList");
                    string serialisedItem = DataItemSerialisation.GetSerialisedDataItem(request);
                    sender.Connect(localEndPoint);
                    
                    // Send data request to server
                    byte[] messageToSend = Encoding.ASCII.GetBytes(serialisedItem + "<EOF>");
                    int byteSent = sender.Send(messageToSend);

                    // Data buffer 
                    byte[] messageReceived = new byte[4096];

                    // Recieve answer from server
                    int byteRecv = sender.Receive(messageReceived);
                    string response = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
                   
                    FlightItemList flightItemList = FlightItemListSerialisation.GetDataItem(response);                         
                    
                   
                    return flightItemList.FlightIDs;
                   

                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    return new List<string>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return new List<string>();
            }
        }
    }
    
}

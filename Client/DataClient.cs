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
    class DataClient
    {

        /// <summary>
        /// Run a DataClient instance.
        /// </summary>
        public void Run()
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
                    DataItem request = new DataItem("Debbie");
                    string serialisedItem = DataItemSerialisation.GetSerialisedDataItem(request);
                    sender.Connect(localEndPoint);
                    Console.WriteLine("Socket connected to -> {0} ",
                                sender.RemoteEndPoint);
                    // Send data request to server
                    byte[] messageToSend = Encoding.ASCII.GetBytes(serialisedItem + "<EOF>");
                    int byteSent = sender.Send(messageToSend);

                    // Data buffer 
                    byte[] messageReceived = new byte[4096];

                    // Recieve answer from server
                    int byteRecv = sender.Receive(messageReceived);
                    string response = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
                    DataItem dataItem = DataItemSerialisation.GetDataItem(response);
                    Console.WriteLine("Received from Server -> {0}", dataItem.Id);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
    
}

using System;
using static System.Console;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Shared;

namespace Server
{
    class Program
    {
        // Main Method 
        static void Main(string[] args)
        {
            new DataServer().Run();
        }
    }

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
                    WriteLine("Text received -> {0} ", dataItem.Id);
                    DataItem response = new DataItem("Green");
                    string serialisedItem = DataItemSerialisation.GetSerialisedDataItem(response);
                    byte[] message = Encoding.ASCII.GetBytes(serialisedItem);

                    clientSocket.Send(message);
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

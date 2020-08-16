using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace SERVER
{
    class TCPModel
    {
        private IPAddress ipAd_of_server;
        private int port_to_listen;
        private TcpListener tcpServer;

        public TCPModel(string ip, int p)
        {
            ipAd_of_server = IPAddress.Parse(ip);
            port_to_listen = p;
            tcpServer = new TcpListener(ipAd_of_server, port_to_listen);
        }
        //function to listen
        public void Listen()
        {
            try
            {
                tcpServer.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
        //accept a new connection
        public Socket SetUpANewConnection(ref int status)
        {
            Socket socket = tcpServer.AcceptSocket();
            status = 1;//QUESTION: WHY we should use status variable here
            return socket;
        }
        //shutdown server
        public void Shutdown()
        {
            //students should develop script to finish the job in progress by working sockets
            tcpServer.Stop();
        }
    }
}

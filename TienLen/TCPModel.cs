using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;


namespace TienLen
{
    class TCPModel
    {
        private TcpClient tcpclnt;
        private Stream stm;
        private byte[] byteSend;
        private byte[] byteReceive;
        private string IPofServer;
        private int port;

        public TCPModel(string ip, int p)
        {
            IPofServer = ip;
            port = p;
            tcpclnt = new TcpClient();
            byteReceive = new byte[100];
        }
        //show information of client to update UI
        public string UpdateInformation()
        {
            string str = "";
            try
            {
                Socket s = tcpclnt.Client;
                str = str + s.LocalEndPoint;
                Console.WriteLine(str);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            return str;
        }
        //Set up a connection to server and get stream for communication
        public int ConnectToServer()
        {
            try
            {
                tcpclnt.Connect(IPofServer, port);
                stm = tcpclnt.GetStream();
                Console.WriteLine("OK!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                return -1;
            }
            return 1;
        }
        //Send data to server after connection is set up
        public void SendData(string str)
        {
            try
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                byteSend = asen.GetBytes(str);
                stm.Write(byteSend, 0, byteSend.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
        public int SendData1(List<int> a)
        {

            try
            {
                string str = "";
                for (int i = 0; i < a.Count() - 1; i++)
                {
                    str = str + a[i] + "\t";
                }
                ASCIIEncoding asen = new ASCIIEncoding();
                byteSend = asen.GetBytes(str);
                stm.Write(byteSend, 0, byteSend.Length);
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                return -1;
            }
        }
        //Read data from server after connection is set up
        public string ReadData()
        {
            string str = "";
            try
            {
                //count the length of data received
                int k = stm.Read(byteReceive, 0, 100);
                Console.WriteLine("Length of data received: " + k.ToString());
                Console.WriteLine("From server: ");
                //conver the byte recevied into string
                char[] c = new char[k];
                for (int i = 0; i < k; i++)
                {
                    Console.Write(Convert.ToChar(byteReceive[i]));
                    c[i] = Convert.ToChar(byteReceive[i]);
                }
                str = new string(c);
            }
            catch (Exception e)
            {
                str = "Error..... " + e.StackTrace;
                Console.WriteLine(str);
            }
            return str;
        }
        //close connection
        public void CloseConnection()
        {
            tcpclnt.Close();
        }
    }
}

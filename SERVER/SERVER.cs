using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace SERVER
{
    public partial class SERVER : Form
    {
        private TCPModel tcp;
        private SocketModel[] socketList;
        private SocketModel[] socketList1;
        private int numberOfPlayers = 200;
        private int currentClient;
        private object thislock;
        public SERVER()
        {
           
            InitializeComponent();
           

        }

        private void SERVER_Load(object sender, EventArgs e)
        {
            this.Text = "SERVER";
            CheckForIllegalCrossThreadCalls = false;
            thislock = new object();
        }



        public void StartServer()
        {
            string ip = textBox1.Text;
            int port = int.Parse(textBox2.Text);
            tcp = new TCPModel(ip, port);
            tcp.Listen();
            button1.Enabled = false;
        }
        public void ServeClients()
        {
            socketList = new SocketModel[numberOfPlayers];
            socketList1 = new SocketModel[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                ServeAClient();
            }
        }
        public void Accept()
        {
            int status = -1;
            Socket s = tcp.SetUpANewConnection(ref status);
            socketList[currentClient] = new SocketModel(s);
            string str = socketList[currentClient].GetRemoteEndpoint();
            string str1 = "New connection from: " + str + "\n";
            textBox3.AppendText(str1);
        }
        public void Accept1()
        {
            int status = -1;
            Socket s = tcp.SetUpANewConnection(ref status);
            socketList1[currentClient] = new SocketModel(s);
        }

        public void ServeAClient()
        {
            int num = -1;
            lock (thislock)
            {
                Accept();
                Accept1();
                currentClient++;
                num = currentClient - 1;
            }
            Thread t = new Thread(Commmunication);
            t.Start(num);
        }
        private int soclient;
        public void Commmunication(object obj)
        {
            int pos = (Int32) obj;
            while (true)
            {
                string str = socketList[pos].ReceiveData();
                if (str == "sansang")
                {
                    soclient++;
                    if (soclient == 4)
                    {
                        ChiaBai();
                    }
                }
                if (str == "100")
                {
                    for (int i = 0; i < currentClient; i++)
                        socketList1[i].SendData("100");
                }
                if (str != "sansang")
                {
                    BroadcastResult(pos, str);

                }
            }
        }
        List<string> listPlayer;
        public void ChiaBai()
        {
            int t1 = 0, t2 = 0, t3 = 0, t4 = 0;
            List<int> p1 = new List<int>();
            List<int> p2 = new List<int>();
            List<int> p3 = new List<int>();
            List<int> p4 = new List<int>();
            listPlayer = new List<string>();
            Random rnd = new Random();
            List<int> lP = new List<int>();
            for (int i = 0; i < 52; i++)
            {
                lP.Add(i);
            }
            for (int i = 0; i < 13; i++)
            {
                t1 = rnd.Next(lP.Count);
                p1.Add(lP[t1]);
                lP.RemoveAt(t1);
            }
            for (int i = 0; i < 13; i++)
            {
                t2 = rnd.Next(lP.Count);
                p2.Add(lP[t2]);
                lP.RemoveAt(t2);
            }
            for (int i = 0; i < 13; i++)
            {
                t3 = rnd.Next(lP.Count);
                p3.Add(lP[t3]);
                lP.RemoveAt(t3);
            }
            for (int i = 0; i < 13; i++)
            {
                t4 = rnd.Next(lP.Count);
                p4.Add(lP[t4]);
                lP.RemoveAt(t4);
            }
            string str1 = ghepchuoi(p1);
            listPlayer.Add(str1);
            string str2 = ghepchuoi(p2);
            listPlayer.Add(str2);
            string str3 = ghepchuoi(p3);
            listPlayer.Add(str3);
            string str4 = ghepchuoi(p4);
            listPlayer.Add(str4);
            for (int i = 0; i < currentClient; i++)
            {

                socketList1[i].SendData(Convert.ToString(13));
                socketList1[i].SendData(listPlayer[i]);
            }
        }
        public string ghepchuoi(List<int> a)
        {
            string str = "";
            for (int i = 0; i < 13; i++)
                str = str + a[i] + "\t";
            return str;
        }
        public void BroadcastResult(int pos, string result)
        {
           // socketList[pos].SendData(result);
            for(int i=0; i < currentClient; i++)
            {
                if(i!=pos)
                {
                    socketList1[i].SendData(result);
                }
            }
        }

        void Button1Click(object sender, EventArgs e)
        {
            StartServer();
            Thread t = new Thread(ServeClients);
            t.Start();
            textBox3.AppendText("SERVER đã khởi động");
        }

       
       
       }
      
    }



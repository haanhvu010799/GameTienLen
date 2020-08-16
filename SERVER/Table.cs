using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SERVER
{
    class Table
    {
        int flag;

        ASCIIEncoding encoding = new ASCIIEncoding();
        TCPModel.Server[] _Local_Server = new TCPModel.Server[5];
        List<PlayerProp> players = new List<PlayerProp>();
        Player _player;
        String rule = "";
        String baiconlai = "";
        String ip = "127.0.0.1";

        public int ID;
        public int NumofPlayer;
        bool check = false;


        public Table(int id, List<PlayerProp> props)
        {
            ID = id;
            players = props;
            NumofPlayer = players.Count;

        }
        void Communication(String _Send_Card, TCPModel.Server server)
        {
            byte[] temp = encoding.GetBytes(_Send_Card);
            server.SendData(temp);
        }
        void XuLyString(String str, ref String str1, ref String str2)
        {
            if (str != null)
            {
                str1 = "";
                str2 = "";
                int i = 0;
                while (str[i] != '$')
                {
                    str1 += str[i];
                    i++;
                }
                str2 = str.Replace(str1, "");
                str2 = str2.Replace("$", "");

            }




        }
        void GetFlag(Player player)
        {

            if (players.Count == 2)
            {
                List<BoBai.Card> card1 = Player.SortCards(player.PlayerCards(1));
                List<BoBai.Card> card2 = Player.SortCards(player.PlayerCards(2));

                if (card1[0].Cards < card2[0].Cards)
                {
                    flag = 1;
                }
                else if (card1[0].Cards == card2[0].Cards)
                {
                    if (card1[0].Type < card2[0].Type)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 2;
                    }
                }
                else
                {
                    flag = 2;
                }
            }
            if (players.Count == 3)
            {
                List<BoBai.Card> card1 = Player.SortCards(player.PlayerCards(1));
                List<BoBai.Card> card2 = Player.SortCards(player.PlayerCards(2));
                List<BoBai.Card> card3 = Player.SortCards(player.PlayerCards(3));

                if (card1[0].Cards < card2[0].Cards && card1[0].Cards < card3[0].Cards)
                {
                    flag = 1;
                }
                else if (card2[0].Cards < card1[0].Cards && card2[0].Cards < card3[0].Cards)
                {
                    flag = 2;
                }
                else if (card1[0].Cards < card2[0].Cards && card1[0].Cards == card3[0].Cards)
                {
                    if (card1[0].Type < card3[0].Type)
                    {
                        flag = 1;
                    }
                    else
                        flag = 3;
                }
                else if (card1[0].Cards == card2[0].Cards && card1[0].Cards < card3[0].Cards)
                {
                    if (card1[0].Type < card2[0].Type)
                    {
                        flag = 1;
                    }
                    else
                        flag = 2;
                }
                else
                    flag = 3;

            }
            if (players.Count == 4)
            {
                List<BoBai.Card> card1 = Player.SortCards(player.PlayerCards(1));
                List<BoBai.Card> card2 = Player.SortCards(player.PlayerCards(2));
                List<BoBai.Card> card3 = Player.SortCards(player.PlayerCards(3));
                List<BoBai.Card> card4 = Player.SortCards(player.PlayerCards(4));
                if (card1[0].Cards == 0)
                {
                    flag = 1;
                }
                if (card2[0].Cards == 0)
                {
                    flag = 2;
                }
                if (card3[0].Cards == 0)
                {
                    flag = 3;
                }
                if (card4[0].Cards == 0)
                {
                    flag = 4;
                }
            }
        }
        void SendToPlayer(Object obj)
        {
            //IPAddress address = IPAddress.Parse(ip);
            //TcpListener _Local_Listenner = new TcpListener(address, prop.Port);
            //_Local_Listenner.Start();
            //Socket _Local_Socket = _Local_Listenner.AcceptSocket();
            //_Local_Server[prop.Index] = new TCPModel.Server(_Local_Socket);

            PlayerProp prop = (PlayerProp)obj;
            _Local_Server[prop.Index] = prop.server;
            GetFlag(_player);
            String data = Player.FormatCardsToSend(_player.PlayerCards(prop.Index));
            Communication(data, prop.server);

        }
        void ChiaBai()
        {
            foreach (var item in players)
            {
                Thread thread = new Thread(SendToPlayer);
                thread.Start(item);
            }
        }
        String ReceiveData(byte[] data)
        {
            if (data != null)
            {
                try
                {

                    return ASCIIEncoding.ASCII.GetString(data);
                }
                catch (Exception)
                {

                    return "0";
                }
            }
            else
            {
                return "";
            }

        }
        public String StrGetCards(int flags)
        {
            switch (flags)
            {
                case 1:
                    return ReceiveData(_Local_Server[1].ReceiveData(1024));

                case 2:
                    return ReceiveData(_Local_Server[2].ReceiveData(1024));

                case 3:
                    return ReceiveData(_Local_Server[3].ReceiveData(1024));

                case 4:
                    return ReceiveData(_Local_Server[4].ReceiveData(1024));

                default:
                    return "0";

            }


        }
        void Broadcast(String str, int f, String rul)
        {
            for (int i = 1; i <= players.Count; i++)
            {
                if (i == f)
                    _Local_Server[i].SendData(encoding.GetBytes("1" + rul + "#" + str));
                else if (f == 20)
                {
                    _Local_Server[i].SendData(encoding.GetBytes(str));
                }
                else
                    _Local_Server[i].SendData(encoding.GetBytes("0" + rul + "#" + str));


            }
        }
        void GetStatus(ref int num, ref int id, ref bool isPlaying)
        {
            num = players.Count();
            id = ID;
            isPlaying = check;

        }
        void XuLiDanhBai()
        {
            int Numberofplayer = players.Count;
            Random random = new Random();
            int Flag = flag;
            int allow = 1;
            switch (Flag)
            {
                case 1:
                    _Local_Server[1].SendData(encoding.GetBytes(allow.ToString()));
                    break;
                case 2:
                    _Local_Server[2].SendData(encoding.GetBytes(allow.ToString()));
                    break;
                case 3:
                    _Local_Server[3].SendData(encoding.GetBytes(allow.ToString()));
                    break;
                case 4:
                    _Local_Server[4].SendData(encoding.GetBytes(allow.ToString()));
                    break;
                default:
                    break;
            }



        }

        public void Start()
        {


            Player player = new Player();
            player.MixCards();
            _player = player;


            ChiaBai();
            while (!check)

            {

                if (players.Count == 2)
                {
                    if (_Local_Server[1] != null && _Local_Server[2] != null)
                    {
                        break;
                    }
                }
                else if (players.Count == 3)
                {
                    if (_Local_Server[1] != null && _Local_Server[2] != null && _Local_Server[3] != null)
                    {
                        break;
                    }

                }
                else if (players.Count == 4)
                {
                    if (_Local_Server[1] != null && _Local_Server[2] != null && _Local_Server[3] != null && _Local_Server[4] != null)
                    {
                        break;
                    }

                }

            }


            Thread.Sleep(50);

            XuLiDanhBai();



            while (true)
            {
                try
                {

                    String card = "";
                    String getcard = StrGetCards(flag);

                    if (getcard == "Win")
                    {
                        Broadcast("Win", flag, "Win");
                        foreach (var item in _Local_Server)
                        {
                            item.Close();
                        }
                        card = "";
                        rule = "";
                        _player = null;
                        check = true;
                        getcard = "";

                        //String a = StrGetCards(flag);
                        break;


                        //01|1|12|1#011|1|12|1
                    }
                    XuLyString(getcard, ref rule, ref card);

                    Broadcast("0", 10, rule);
                    if (card != "")
                    {


                        flag++;
                        if (flag == players.Count + 1)
                        {
                            flag = 1;
                        }

                        Broadcast(card, flag, rule);




                    }

                }
                catch (Exception)
                {
                    continue;
                }


            }









        }
    }
}

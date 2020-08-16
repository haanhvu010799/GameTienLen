using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace TienLen
{
    public partial class Play : Form
    {
       
        private int play1;
        private int play3;
        private int play4;
        int[] arrStt;   //mảng trạng thái 1, 0
        List<int> listGO;

        List<int> listTable = new List<int>();
        Point[] loc1 = new Point[13] { new Point(10, 33), new Point(52, 33) , new Point(94, 33), new Point(136, 33),
                                       new Point(179, 33),new Point(222, 33),new Point(264, 33),new Point(312,33),new Point(358,33),
                                      new Point(401,33),new Point(443,33),new Point(484,33),new Point(526,33)};
        Point[] loc2 = new Point[13] { new Point(10, 3), new Point(52, 3) , new Point(94, 3), new Point(136, 3),
                                       new Point(179, 3),new Point(222, 3),new Point(264, 3),new Point(312,3),new Point(358,3),
                                      new Point(401,3),new Point(443,3),new Point(484,3),new Point(526,3)};
        
        public Play()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
            Playing();
            prb1.Step = Const.countDownStep;
            prb1.Maximum = Const.countDownTime;
            prb1.Value = 0;
            prb2.Step = Const.countDownStep;
            prb2.Maximum = Const.countDownTime;
            prb2.Value = 0;
            prb3.Step = Const.countDownStep;
            prb3.Maximum = Const.countDownTime;
            prb3.Value = 0;
            prb4.Step = Const.countDownStep;
            prb4.Maximum = Const.countDownTime;
            prb4.Value = 0;

            timer1.Interval = Const.countDownInterval;
            timer2.Interval = Const.countDownInterval;
            timer3.Interval = Const.countDownInterval;
            timer4.Interval = Const.countDownInterval;
        }
        private TCPModel tcpForPlayer;
        private TCPModel tcpForOpponent;
        public void Connect()
        {
            string ip = "127.0.0.1";
            int port = 13000;
            tcpForPlayer = new TCPModel(ip, port);
            tcpForPlayer.ConnectToServer();
            this.Text = tcpForPlayer.UpdateInformation();
            tcpForOpponent = new TCPModel(ip, port);
            tcpForOpponent.ConnectToServer();
        }
        private void btnready_Click(object sender, EventArgs e)
        {
            tcpForPlayer.SendData("sansang");
            btnready.Enabled = false;
        }
        public void player1Next()
        {


            //thoi gian chay 
            timer4.Stop();
            timer3.Stop();
            timer2.Stop();
            prb2.Visible = false;
            prb3.Visible = false;
            prb4.Visible = false;
            prb1.Visible = true;
            if (listGO == listTable || listTable == null)
            {
                removeAll(listTable);
                btndanh.Enabled = true;
                btnNext.Enabled = false;
            }
            if (listGO != listTable)
            {
                btndanh.Enabled = true;
                btnNext.Enabled = true;
            }
            timer1.Start();
            prb1.Value = 0;

        }
        public void player2Next()
        {


            //thoi gian chay 
            timer4.Stop();
            timer3.Stop();
            timer1.Stop();
            prb1.Visible = false;
            prb3.Visible = false;
            prb4.Visible = false;
            prb2.Visible = true;
            if (listGO == listTable || listTable == null)
            {
                removeAll(listTable);
                btndanh.Enabled = true;
                btnNext.Enabled = false;
            }
            if (listGO != listTable)
            {
                btndanh.Enabled = true;
                btnNext.Enabled = true;
            }
            timer2.Start();
            prb2.Value = 0;

        }
        public void player3Next()
        {


            //thoi gian chay 
            timer4.Stop();
            timer1.Stop();
            timer2.Stop();
            prb2.Visible = false;
            prb1.Visible = false;
            prb4.Visible = false;
            prb3.Visible = true;
            if (listGO == listTable || listTable == null)
            {
                removeAll(listTable);
                btndanh.Enabled = true;
                btnNext.Enabled = false;
            }
            if (listGO != listTable)
            {
                btndanh.Enabled = true;
                btnNext.Enabled = true;
            }
            timer3.Start();
            prb3.Value = 0;

        }
        public void player4Next()
        {


            //thoi gian chay 
            timer1.Stop();
            timer3.Stop();
            timer2.Stop();
            prb2.Visible = false;
            prb3.Visible = false;
            prb1.Visible = false;
            prb4.Visible = true;
            if (listGO == listTable || listTable == null)
            {
                removeAll(listTable);
                btndanh.Enabled = true;
                btnNext.Enabled = false;
            }
            if (listGO != listTable)
            {
                btndanh.Enabled = true;
                btnNext.Enabled = true;
            }
            timer4.Start();
            prb4.Value = 0;

        }
        public void Playing()
        {
            Thread t = new Thread(NhanBai);
            t.Start();
        }

        List<int> listPlayer;
        public void NhanBai()
        {
            while (true)
            {
                int a = Convert.ToInt32(tcpForOpponent.ReadData());
                if (a == 13)
                {
                    string str = tcpForOpponent.ReadData();
                    arrStt = new int[13];
                    //textBox2.Text = str;
                    listPlayer = new List<int>();
                    List<string> QB = new List<string>();
                    string[] re = re = str.Split('\t');
                    //re = str.Split('\t');

                    for (int i = 0; i < 13; i++)
                        listPlayer.Add(Convert.ToInt32(re[i]));
                    listPlayer.Sort();
                    {
                        foreach (PictureBox p in this.pnlPlayer.Controls)
                        {
                            for (int i = 0; i < listPlayer.Count; i++)
                            {
                                if (p.Name == "p" + i)
                                    p.Image = getImg(listPlayer[i]);
                            }
                        }
                    }
                }
                if (a < 12)
                {
                    removeAll(listTable);
                    string str = tcpForOpponent.ReadData();
                    arrStt = new int[13];
                    //textBox2.Text = str;
                    listPlayer= new List<int>();
                    List<string> QB = new List<string>();
                    string[] re = re = str.Split('\t');
                    for (int i = 0; i < a; i++)
                        listTable.Add(Convert.ToInt32(re[i]));
                    foreach (PictureBox p in this.pnlTableCards.Controls)
                    {
                        for (int i = 0; i < listTable.Count; i++)
                        {
                            if (p.Name == "t" + i)
                                p.Image = getImg(listTable[i]);
                        }
                        for (int j = a; j < 12; j++)
                            if (p.Name == "t" + j)
                            {
                                p.Visible = false;
                            }
                    }
                }


            }

        }
        public int kiemtra(string[] sa)
        {
            int a = 0;
            for (int i = 0; i < 13; i++)
            {
                if (sa[i] != null)
                    a++;
            }
            return a;
        }
        //private void Play_Load(object sender, EventArgs e)
        //{
        //    CheckForIllegalCrossThreadCalls = false;
        //    Connect();
        //    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        //    this.SetStyle(ControlStyles.UserPaint, true);
        //    this.SetStyle(ControlStyles.DoubleBuffer, true);
        //    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);


        //    newGame();
        //    exception();    //xét ăn trắng
        //    if (listPlayer[0] == 0)   //3 bích máy đánh trc
        //    {
        //        playerNext();
        //    }


        //}


        //public void newGame()
        //{
        //    timer1.Stop();
        //    //
        //    listPlayer = new List<int>();

        //    arrStt = new int[13];
        //    //đổ giá trị
        //    Random rnd = new Random();
        //    int t1 = 0;
        //    List<int> lP = new List<int>();
        //    for (int i = 0; i < 52; i++)
        //    {
        //        lP.Add(i);
        //    }
        //    for (int i = 0; i < 13; i++)
        //    {
        //        t1 = rnd.Next(lP.Count);
        //        listPlayer.Add(lP[t1]);
        //        lP.RemoveAt(t1);
        //    }

        //    //sắp bài
        //    listPlayer.Sort();

        //    //đổ ảnh player
        //    foreach (PictureBox pl in this.pnlPlayer.Controls)
        //    {
        //        for (int i = 0; i < listPlayer.Count; i++)
        //        {
        //            if (pl.Name == "p" + i)
        //                pl.Image = getImg(listPlayer[i]);
        //        }
        //    }




        //}
        List<int> lstTest;
        public void exception() //ăn trắng (lốc 12 con hoặc 4 con heo hoặc 6 đôi)
        {

            lstTest = new List<int>();
            
            if (checkLoc(listPlayer, 0, 12, ref lstTest) || check4(listPlayer, 9, ref lstTest) || SixPairs(listPlayer))
            {
                //mở các lá bài lên
               
                MessageBox.Show("Bạn ăn trắng!! Bạn đã thắng!!");
                this.Close();
                Application.Restart();
            }
        }
        public bool SixPairs(List<int> l) //ktra 6 đôi ăn trắng
        {
            for (int i = 0; i < 11; i++)
            {
                if (i % 2 == 0)
                {
                    if (getRank(l[i]) != getRank(l[i + 1]))
                        return false;
                }
            }
            return true;
        }
        //lấy ảnh lá bài
        Image getImg(int i)
        {
            Image img;
            if (i > 51)
                img = null;
            else
                img = Image.FromFile("Resources\\" + i + ".png");
            return img;
        }   
        // click bai
        private void card_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            int i = Int32.Parse(pic.Name.Remove(0, 1));
            if (pic.Location == loc1[i])
            {
                pic.Location = loc2[i];
                arrStt[i] = 1; // len
            }
            else
            {
                pic.Location = loc1[i];
                arrStt[i] = 0; // xuong
            }

        }
        //btn đánh
        private void btndanh_Click_1(object sender, EventArgs e)
        {
            listGO = new List<int>();
            for (int i = 0; i < listPlayer.Count; i++)
            {
                if (arrStt[i] == 1)
                {
                    listGO.Add(listPlayer[i]);
                }
            }
            if (isValid(listGO))
            {
                string str = ghepchuoi(listGO, listGO.Count);
                tcpForPlayer.SendData(Convert.ToString(listGO.Count));
                tcpForPlayer.SendData(str);
                K(listGO.Count(), listGO);
                boqua();
            }
        }
        public string ghepchuoi(List<int> a, int b)
        {
            string str = "";
            for (int i = 0; i < b; i++)
                str += a[i].ToString() + "\t";
            return str;
        }
        //ktra hợp lệ
        public bool isValid(List<int> a)
        {
            if (listTable.Count() == 0)    //bắt đầu chơi hoặc đối thủ bỏ lượt
            {
                switch (a.Count())
                {
                    case 1:
                        return true;
                    case 2:
                        if (isSameRank(a))
                            return true;
                        return false;
                    case 3:
                        if (isSameRank(a))
                            return true;
                        if (isContinuous(a))
                            return true;
                        return false;
                    case 4:
                        if (isContinuous(a))
                            return true;
                        if (isSameRank(a))
                            return true;
                        return false;
                    case 5:
                        if (isContinuous(a))
                            return true;
                        return false;
                    case 6:
                        if (isContinuous(a))
                            return true;
                        if (isConsecutivePairs(a) == 3)
                            return true;
                        return false;
                    case 7:
                        if (isContinuous(a))
                            return true;
                        return false;
                    case 8:
                        if (isContinuous(a))
                            return true;
                        if (isConsecutivePairs(a) == 4)
                            return true;
                        return false;
                    case 9:
                        if (isContinuous(a))
                            return true;
                        return false;
                    case 10:
                        if (isContinuous(a))
                            return true;
                        if (isConsecutivePairs(a) == 5)
                            return true;
                        return false;
                    case 11:
                        if (isContinuous(a))
                            return true;
                        return false;
                    default:
                        return false;
                }
            }
            else   // theo lượt
            {
                switch (a.Count())
                {
                    case 1:
                        if (listTable.Count == 1 && a[0] > listTable[0])
                            return true;
                        return false;
                    case 2:
                        if (listTable.Count == 2 && isSameRank(a) && a[1] > listTable[1])
                            return true;
                        return false;
                    case 3:
                        if (listTable.Count == 3)
                        {
                            if (isSameRank(listTable) && isSameRank(a) && a[2] > listTable[2])
                                return true;
                            if (isContinuous(listTable) && isContinuous(a) && a[2] > listTable[2])
                                return true;
                        }
                        return false;
                    case 4:
                        if (listTable.Count == 1 && getRank(listTable[0]) == 12 && isSameRank(a))  //(tứ quý)chặt 1 heo
                            return true;
                        if (listTable.Count == 2 && getRank(listTable[0]) == 12 && isSameRank(a))  //(tứ quý)chặt 2 heo
                            return true;
                        if (listTable.Count == 6 && isConsecutivePairs(listTable) == 3 && isSameRank(a))   //(tứ quý)chặt 3 đôi thông
                            return true;
                        if (listTable.Count == 4)
                        {
                            if (isContinuous(listTable) && isContinuous(a) && a[3] > listTable[3])
                                return true;
                            if (isSameRank(listTable) && isSameRank(a) && a[3] > listTable[3])
                                return true;
                        }
                        return false;
                    case 5:
                        if (listTable.Count == 5 && isContinuous(listTable) && isContinuous(a) && a[4] > listTable[4])
                            return true;
                        return false;
                    case 6:
                        if (listTable.Count == 1 && getRank(listTable[0]) == 12 && isConsecutivePairs(a) == 3)  //(3 đôi thông)chặt 1 heo
                            return true;
                        if (listTable.Count == 6)
                        {
                            if (isContinuous(listTable) && isContinuous(a) && a[5] > listTable[5])
                                return true;
                            if (isConsecutivePairs(listTable) == 3 && isConsecutivePairs(a) == 3 && a[5] > listTable[5])
                                return true;
                        }
                        return false;
                    case 7:
                        if (listTable.Count == 7 && isContinuous(a) && isContinuous(listTable) && a[6] > listTable[6])
                            return true;
                        return false;
                    case 8:
                        if (listTable.Count == 1 && getRank(listTable[0]) == 12 && isConsecutivePairs(a) == 4)  //(4 đôi thông)chặt 1 heo
                            return true;
                        if (listTable.Count == 2 && getRank(listTable[0]) == 12 && isConsecutivePairs(a) == 4)  //(4 đôi thông)chặt 2 heo
                            return true;
                        if (listTable.Count == 4 && isSameRank(listTable) && isConsecutivePairs(a) == 4)   //(4 đôi thông)chặt tứ quý
                            return true;
                        if (listTable.Count == 6 && isConsecutivePairs(a) == 3 && isConsecutivePairs(a) == 4)    //(4 đôi thông)chặt 3 đôi thông
                            return true;
                        if (listTable.Count == 8)
                        {
                            if (isContinuous(listTable) && isContinuous(a) && a[7] > listTable[7])
                                return true;
                            if (isConsecutivePairs(listTable) == 4 && isConsecutivePairs(a) == 4 && a[7] > listTable[7])
                                return true;
                        }
                        return false;
                    case 9:
                        if (listTable.Count == 9 && isContinuous(a) && isContinuous(listTable) && a[8] > listTable[8])
                            return true;
                        return false;
                    case 10:
                        if (listTable.Count == 1 && getRank(listTable[0]) == 12 && isConsecutivePairs(a) == 5)  //(5 đôi thông)chặt 1 heo
                            return true;
                        if (listTable.Count == 2 && getRank(listTable[0]) == 12 && isConsecutivePairs(a) == 5)  //(5 đôi thông)chặt 2 heo
                            return true;
                        if (listTable.Count == 4 && isSameRank(listTable) && isConsecutivePairs(a) == 5)   //(5 đôi thông)chặt tứ quý
                            return true;
                        if (listTable.Count == 6 && isConsecutivePairs(listTable) == 3 && isConsecutivePairs(a) == 5)    //(5 đôi thông)chặt 3 đôi thông
                            return true;
                        if (listTable.Count == 8 && isConsecutivePairs(listTable) == 4 && isConsecutivePairs(a) == 5)    //(5 đôi thông)chặt 4 đôi thông
                            return true;
                        if (listTable.Count == 10)
                        {
                            if (isContinuous(listTable) && isContinuous(a) && a[9] > listTable[9])
                                return true;
                            if (isConsecutivePairs(listTable) == 5 && isConsecutivePairs(a) == 5 && a[9] > listTable[9])
                                return true;
                        }
                        return false;
                    case 11:
                        if (listTable.Count == 11 && isContinuous(a) && isContinuous(listTable) && a[10] > listTable[10])
                            return true;
                        return false;
                    default:
                        return false;
                }
            }
        }
        //ktra 3 - 4 - 5 đôi thông
        public int isConsecutivePairs(List<int> l)
        {
            int k = (int)l.Count() / 2;
            for (int i = 0; i < (l.Count() - 1); i++)
            {
                if (i % 2 == 0)
                {
                    if (getRank(l[i]) != getRank(l[i + 1]))
                        return -1;
                }
                else
                {
                    if (getRank(l[i + 1]) != (getRank(l[i]) + 1))
                        return -1;
                }
            }
            return k;
        }
        //ktra cùng bộ
        public bool isSameRank(List<int> l)
        {
            for (int i = 0; i < (l.Count() - 1); i++)
                if (getRank(l[i]) != getRank(l[i + 1]))
                    return false;
            return true;
        }
        //ktra lốc liên tục
        public bool isContinuous(List<int> l)
        {
            if (getRank(l[l.Count - 1]) == 12)
                return false;
            for (int i = 0; i < (l.Count() - 1); i++)
                if (getRank(l[i + 1]) != (getRank(l[i]) + 1))
                    return false;
            return true;
        }
        //bộ
        public int getRank(int k)
        {
            int s = (int)k / 4;
            return s;
        }
        //Người đánh bài
        public void K(int i, List<int>test)
        {
            //reset Table
            removeAll(listTable);
            //chuyển vào list Table
            for (int j = 0; j < listGO.Count; j++)
                listTable.Add(listGO[j]);
            //chuyển ảnh lên Table
            foreach (PictureBox cT in this.pnlTableCards.Controls)
            {
                for (int j = 0; j < i; j++)
                    if (cT.Name == "t" + j)
                    {
                        cT.Visible = true;
                        cT.Image = getImg(listTable[j]);
                    }
                for (int j = i; j < 12; j++)
                    if (cT.Name == "t" + j)
                    {
                        cT.Visible = false;
                    }
            }
            //remove giá trị trong listPlayer, reset mảng arrStt
            arrStt = new int[13];
            for (int j = 0; j < listTable.Count; j++)
            {
                listPlayer.Remove(listTable[j]);
            }
            //đổ ảnh player
            foreach (PictureBox pl in this.pnlPlayer.Controls)
            {
                pl.Image = null;
                pl.Enabled = false;
                for (int j = 0; j < listPlayer.Count; j++)
                {
                    if (pl.Name == "p" + j)
                    {
                        pl.Image = getImg(listPlayer[j]);
                        pl.Enabled = true;
                    }
                }
            }
            //reset location
            foreach (PictureBox loc in this.pnlPlayer.Controls)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (loc.Name == "p" + j)
                        loc.Location = loc1[j];
                }
            }
            isPlayerWIN();
        }
        public void playerNext()    //lượt người
        {

            prb1.Visible = true;
            //pnlPlayer.Enabled = true;
            pnlButton.Enabled = true;
            //prbCountDown.Enabled = true;

            timer1.Start();
            prb1.Value = 0;
        }

        
        public void removeAll(List<int> l)  //remove all
        {
            for (int i = 0; i < l.Count; i++)
            {
                l.RemoveAt(i);
                i--;
            }
        }
        List<int> lstcheck2;
        public bool check3pairs(List<int> l, ref List<int> ll)  //3 đôi thông
        {
            lstcheck2 = new List<int>();

            for (int i = 0; i < l.Count - 5; i++)
            {
                if (getRank(l[i]) == getRank(l[i + 1]) && getRank(l[i + 2]) == getRank(l[i + 3]) && getRank(l[i + 4]) == getRank(l[i + 5])
                && getRank(l[i + 2]) == getRank(l[i]) + 1 && getRank(l[i + 4]) == getRank(l[i + 2]) + 1)
                {
                    for (int j = i; j < i + 6; j++)
                        ll.Add(l[j]);
                    return true;
                }
                if (i < l.Count - 6 && getRank(l[i]) == getRank(l[i + 1]) && getRank(l[i + 2]) == getRank(l[i + 3]) && getRank(l[i + 2]) == getRank(l[i + 4])
                    && getRank(l[i + 5]) == getRank(l[i + 6]) && getRank(l[i + 2]) == getRank(l[i]) + 1 && getRank(l[i + 5]) == getRank(l[i + 2]) + 1)
                {
                    ll.Add(l[i]);
                    ll.Add(l[i + 1]);
                    ll.Add(l[i + 2]);
                    ll.Add(l[i + 3]);
                    ll.Add(l[i + 5]);
                    ll.Add(l[i + 6]);
                    return true;
                }
            }
            return false;
        }
        public bool check4pairs(List<int> l, ref List<int> ll)  //4 đôi thông
        {
            lstcheck2 = new List<int>();
            for (int i = 0; i < l.Count - 7; i++)
            {
                if (getRank(l[i]) == getRank(l[i + 1]) && getRank(l[i + 2]) == getRank(l[i + 3]) && getRank(l[i + 4]) == getRank(l[i + 5]) && getRank(l[i + 6]) == getRank(l[i + 7])
                    && getRank(l[i + 2]) == getRank(l[i]) + 1 && getRank(l[i + 4]) == getRank(l[i + 2]) + 1 && getRank(l[i + 6]) == getRank(l[i + 4]) + 1)
                {
                    for (int j = i; j < i + 8; j++)
                        ll.Add(l[j]);
                    return true;
                }
                if (i < l.Count - 8 && getRank(l[i]) == getRank(l[i + 1]) && getRank(l[i + 2]) == getRank(l[i + 3]) && getRank(l[i + 2]) == getRank(l[i + 4]) && getRank(l[i + 5]) == getRank(l[i + 6]) && getRank(l[i + 7]) == getRank(l[i + 8])
                    && getRank(l[i + 2]) == getRank(l[i]) + 1 && getRank(l[i + 5]) == getRank(l[i + 2]) + 1 && getRank(l[i + 7]) == getRank(l[i + 5]) + 1)
                {
                    ll.Add(l[i]);
                    ll.Add(l[i + 1]);
                    ll.Add(l[i + 2]);
                    ll.Add(l[i + 3]);
                    ll.Add(l[i + 5]);
                    ll.Add(l[i + 6]);
                    ll.Add(l[i + 7]);
                    ll.Add(l[i + 8]);
                    return true;
                }
                if (i < l.Count - 8 && getRank(l[i]) == getRank(l[i + 1]) && getRank(l[i + 2]) == getRank(l[i + 3]) && getRank(l[i + 4]) == getRank(l[i + 5]) && getRank(l[i + 4]) == getRank(l[i + 6]) && getRank(l[i + 7]) == getRank(l[i + 8])
                    && getRank(l[i + 2]) == getRank(l[i]) + 1 && getRank(l[i + 4]) == getRank(l[i + 2]) + 1 && getRank(l[i + 7]) == getRank(l[i + 4]) + 1)
                {
                    ll.Add(l[i]);
                    ll.Add(l[i + 1]);
                    ll.Add(l[i + 2]);
                    ll.Add(l[i + 3]);
                    ll.Add(l[i + 4]);
                    ll.Add(l[i + 5]);
                    ll.Add(l[i + 7]);
                    ll.Add(l[i + 8]);
                    return true;
                }
                if (i < l.Count - 9 && getRank(l[i]) == getRank(l[i + 1]) && getRank(l[i + 2]) == getRank(l[i + 3]) && getRank(l[i + 2]) == getRank(l[i + 4])
                    && getRank(l[i + 5]) == getRank(l[i + 6]) && getRank(l[i + 5]) == getRank(l[i + 7]) && getRank(l[i + 8]) == getRank(l[i + 9])
                    && getRank(l[i + 2]) == getRank(l[i]) + 1 && getRank(l[i + 5]) == getRank(l[i + 2]) + 1 && getRank(l[i + 8]) == getRank(l[i + 5]) + 1)
                {
                    ll.Add(l[i]);
                    ll.Add(l[i + 1]);
                    ll.Add(l[i + 2]);
                    ll.Add(l[i + 3]);
                    ll.Add(l[i + 5]);
                    ll.Add(l[i + 6]);
                    ll.Add(l[i + 8]);
                    ll.Add(l[i + 9]);
                    return true;
                }
            }
            return false;
        }
        public bool check2(List<int> l, int i, ref List<int> ll)    //đôi
        {
            if (i < (l.Count - 1) && getRank(l[i]) == getRank(l[i + 1]))
            {
                ll.Add(l[i]);
                ll.Add(l[i + 1]);
                return true;
            }
            return false;
        }
        public bool check3(List<int> l, int i, ref List<int> ll)    //ba
        {
            if (i < (l.Count - 2) && getRank(l[i]) == getRank(l[i + 1]) && getRank(l[i]) == getRank(l[i + 2]))
            {
                ll.Add(l[i]);
                ll.Add(l[i + 1]);
                ll.Add(l[i + 2]);
                return true;
            }
            return false;
        }
        public bool check4(List<int> l, int i, ref List<int> ll)    //tứ quý
        {
            if (i < (l.Count - 3) && getRank(l[i]) == getRank(l[i + 1]) && getRank(l[i]) == getRank(l[i + 2]) && getRank(l[i]) == getRank(l[i + 3]))
            {
                ll.Add(l[i]);
                ll.Add(l[i + 1]);
                ll.Add(l[i + 2]);
                ll.Add(l[i + 3]);
                return true;
            }
            return false;
        }
        List<int> lstcheck;
        public bool checkLoc(List<int> lst, int i, int loc, ref List<int> ll) //lốc
        {
            int end = 0;
            if (i > (lst.Count - loc))
                return false;
            else
            {
                lstcheck = new List<int>();

                int k = i;
                for (int j = 0; j < loc; j++)
                {
                    lstcheck.Add(lst[k]);
                    end = lst[k];
                    if (getRank(end) == 12)
                        return false;
                    if (j != (loc - 1) && k == lst.Count - 1)
                        return false;
                    while (k < lst.Count - 1)
                    {
                        if (getRank(lst[k]) == getRank(lst[k + 1]))
                        {
                            if (j == (loc - 1))
                                end = lst[k + 1];
                            k++;
                        }
                        else
                        {
                            if (getRank(lst[k + 1]) == getRank(lst[k]) + 1)
                            {
                                k++;
                                break;
                            }
                            else
                            {
                                if (j == loc - 1)
                                    break;
                                else
                                    return false;
                            }
                        }
                    }
                }
                for (int j = 0; j < (loc - 1); j++)
                {
                    ll.Add(lstcheck[j]);
                }
                ll.Add(end);
                return true;
            }
        }
       
        public void boqua()
        {
            //reset Table
            for (int j = 0; j < listTable.Count; j++)
            {
                listTable.RemoveAt(j);
                j--;
            }
        }
       
        //btn bỏ qua
        private void btnNext_Click(object sender, EventArgs e)  //bỏ qua
        {

            //reset Table
            for (int j = 0; j < listTable.Count; j++)
            {
                listTable.RemoveAt(j);
                j--;
            }
        }
        public void isPlayerWIN()
        {
            if (listPlayer.Count == 0)
            {
                timer1.Stop();

                prb1.Visible = false;



                MessageBox.Show("Bạn đã thắng!", "You win", MessageBoxButtons.OK);
                this.Close();
                Application.Restart();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            prb1.PerformStep();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            prb2.PerformStep();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            prb3.PerformStep();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            prb4.PerformStep();
        }
    }
}


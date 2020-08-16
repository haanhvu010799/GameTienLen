using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
namespace SERVER
{
    class PlayerProp
    {
        public TCPModel.Server server;
        public int Index;
        public PlayerProp(TCPModel.Server _server, int index)
        {
            server = _server;
            Index = index;
        }
    }
}

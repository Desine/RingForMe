using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server;

public class Client
{
    public Socket socket;
    public Core.Receiver receiver = new();
    public Core.Sender sender = new();
    public Info info;


    public struct Info
    {
        public int id;
        public string name;
    }

}

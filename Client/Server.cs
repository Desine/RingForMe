using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client;

public class Server
{
    public IPEndPoint ipEndPoint;
    public Socket socket;
    public Core.Reciever reciever = new();
    public Core.Sender sender = new();

}

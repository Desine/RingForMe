using Core;
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
    public string address = "26.68.144.220";
    public int port = 2222;
    public IPEndPoint ipEndPoint;
    public Socket socket;
    public Core.Receiver receiver = new();
    public Core.Sender sender = new();


    public static Action OnConnectedToServer = () => { };


    public async Task ConnectToServer(IPEndPoint iPEndPoint)
    {
        ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
        socket = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        await socket.ConnectAsync(ipEndPoint);
        OnConnectedToServer();
        _ = Task.Run(() => receiver.ReceiveMessageAsync(socket));
    }

}

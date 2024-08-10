using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client;

public class Client
{

    public Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    
    public Server server = new();



    public async Task ConnectToServer(IPEndPoint iPEndPoint)
    {
        server.ipEndPoint = new IPEndPoint(IPAddress.Parse(server.address), server.port);
        server.socket = new(server.ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        await socket.ConnectAsync(server.ipEndPoint);
        _ = Task.Run(() => server.receiver.ReceiveMessageAsync(server.socket));
    }


}

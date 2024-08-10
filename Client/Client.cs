using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client;

public class Client
{

    public Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    
    public string serverAddress = "26.68.144.220";
    public int serverPort = 2222;
    public Server server = new();



    public void Instantiate()
    {
        server.ipEndPoint = new IPEndPoint(IPAddress.Parse(serverAddress), serverPort);
        server.socket = new(server.ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    }


    public async Task ConnectToServer(IPEndPoint iPEndPoint)
    {
        await clientSocket.ConnectAsync(server.ipEndPoint);
        _ = Task.Run(() => server.receiver.ReceiveMessageAsync(server.socket));
    }


}

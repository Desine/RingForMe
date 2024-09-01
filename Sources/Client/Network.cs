using System.Net;
using System.Net.Sockets;

namespace Client;

public class Network
{

    public Network()
    {
        ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
        socket = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    }


    public string address = "26.68.144.220";
    public int port = 2222;
    public IPEndPoint ipEndPoint;
    public Socket socket;
    public Core.Receiver receiver = new();
    public Core.Sender sender = new();

    public Action OnConnectedToServer = () => { };
    public Action OnMessageReceived = () => { };



    public void ConnectToServer()
    {
        _ = Task.Run(() => ConnectToServerAsync());
    }
    public async Task ConnectToServerAsync()
    {
        await socket.ConnectAsync(ipEndPoint);
        OnConnectedToServer();
        await receiver.ReceiveMessageAsync(socket);
    }

    public void SendMessage(string message)
    {
        sender.SendMessage(socket, message);
    }

    public string ServerInfo()
    {
        return ipEndPoint.ToString();
    }






}

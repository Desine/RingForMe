using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Server;

public static class Server
{
    public static string localAddress;
    public static int port = 2222;
    public static IPEndPoint localIPEndPoint = new IPEndPoint(IPAddress.Any, port);
    public static Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


    public static List<Client> clients = new();
    public static Action<Client> OnClientAccepted = _ => { };


    public static void Instantiate()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localAddress = ip.ToString();
                break;
            }
        }

        listenSocket.Bind(localIPEndPoint);
        listenSocket.Listen(10);
    }

    public static async Task Accept()
    {
        while (true)
        {
            Socket acceptedSocket = await listenSocket.AcceptAsync();

            Client client = new Client();
            client.socket = acceptedSocket;
            clients.Add(client);
            OnClientAccepted(client);
            _ = Task.Run(() => client.receiver.ReceiveMessageAsync(acceptedSocket));
        }
    }





    public static void SendMessageToClient(string clientName, string message)
    {
        foreach (var client in clients)
        {
            if (client.info.name == clientName)
            {
                client.sender.SendMessage(client.socket, message);
            }
        }
    }
    public static void SendMessageToAllClients(string message)
    {
        foreach (var client in clients)
        {
            client.sender.SendMessage(client.socket, message);
        }
    }



}

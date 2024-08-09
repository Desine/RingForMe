using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Server;

public class Server
{
    public string localAddress;
    public Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


    public List<Client> clients = new();
    public Action<Client> OnClientAccepted = _ => { };


    public void Instantiate()
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

        int port = 2222;
        IPEndPoint localIPEndPoint = new IPEndPoint(IPAddress.Any, port);
        listenSocket.Bind(localIPEndPoint);
        listenSocket.Listen(10);
    }

    public async Task Accept()
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





    public void SendMessageToClient(string clientName, string message)
    {
        foreach (var client in clients)
        {
            if (client.name == clientName)
            {
                client.sender.SendMessage(client.socket, message);
            }
        }
    }
    public void SendMessageToAllClients(string message)
    {
        foreach (var client in clients)
        {
            client.sender.SendMessage(client.socket, message);
        }
    }



}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server;

public class Network
{
    public string localAddress;
    public int port = 2222;
    public IPEndPoint localIPEndPoint;
    public Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


    public List<Client> clients = new();
    public Action<Client> OnClientAccepted = _ => { };


    public void Instantiate()
    {
        localIPEndPoint = new IPEndPoint(IPAddress.Any, port);
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
            if (client.info.name == clientName)
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

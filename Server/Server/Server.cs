using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Server;

public class Server
{
    public string localAddress;
    public Socket listenSocket;


    public List<Client> clients = new();



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

        listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public async Task Accept()
    {
        while (true)
        {
            Socket acceptedSocket = await listenSocket.AcceptAsync();

            HandleNewClient(acceptedSocket);
        }
    }



    public void HandleNewClient(Socket socket)
    {
        Client client = new Client();
        clients.Add(client);
        client.socket = socket;
        _ = Task.Run(() => client.reciever.RecieveMessageAsync(socket));
    }





    public void SendMessage(string clientName, Core.WebDataProtocol.Message message)
    {
        foreach (var client in clients)
        {
            if (client.name == clientName)
            {
                string strMessage = JsonSerializer.Serialize(message);
                client.sender.SendMessage(client.socket, strMessage);
            }
        }
    }



}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

public class Server
{
    string localAddress;
    Socket listenSocket;


    List<Client> clients = new();



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
        _ = Task.Run(() => client.reciever.Recieve(socket));
    }


}

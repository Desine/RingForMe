using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Core;

namespace Server;

public class Server
{
    string localAddress;
    Socket listenSocket;


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

            _ = Task.Run(() => HandleClient(acceptedSocket));
        }
    }



    public async Task HandleClient(Socket acceptedSocket)
    {


        Task receiveTask = ReceiveMessagesAsync(socket);
        Task sendTask = SendMessagesAsync(socket);

        await Task.WhenAny(receiveTask, sendTask);

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }



}

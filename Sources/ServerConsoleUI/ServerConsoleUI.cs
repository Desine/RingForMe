



Server.Server.Instantiate();
_ = Task.Run(() => Server.Server.Accept());

Server.Server.OnClientAccepted += Server_OnClientAccepted;
void Server_OnClientAccepted(Server.Client client)
{
    Console.WriteLine("Client connected");
    client.receiver.OnReceived += WriteReceivedMessage;
}
void WriteReceivedMessage(string message)
{
    Console.WriteLine("Received message: " + message);
}




while (true)
{
    Console.WriteLine("Options:" +
        "\n1 - Send message to client" +
        "\n2 - Send message to all clients" +
        "\n3 - Connected clients" +
        "\n4 - Server info"
        );

    string choise = Console.ReadLine();
    switch (choise)
    {
        case "1":
            SendMessageToClient();
            break;
        case "2":
            SendMessageToAllClients();
            break;
        case "3":
            ConnectedClients();
            break;
        case "4":
            ServerInfo();
            break;
    }
}




void SendMessageToClient()
{
    Console.Write("Send to: ");
    string recipient = Console.ReadLine();
    Console.Write("Message: ");
    string message = Console.ReadLine();

    Server.Server.SendMessageToClient(recipient, message);
}
void SendMessageToAllClients()
{
    Console.Write("Message: ");
    string message = Console.ReadLine();

    Server.Server.SendMessageToAllClients(message);
}

void ConnectedClients()
{
    foreach (var client in Server.Server.clients)
    {
        Console.WriteLine("client namae: " + client.info.name);
        Console.WriteLine("client RemoteEndPoint: " + client.socket.RemoteEndPoint);
    }
}
void ServerInfo()
{
        Console.WriteLine("Server local address: " + Server.Server.localAddress);
        Console.WriteLine("Server port: " + Server.Server.port);
        Console.WriteLine("Server local IP end point: " + Server.Server.localIPEndPoint);
}




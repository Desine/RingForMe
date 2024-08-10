


Server.Server server = new();
server.Instantiate();
_ = Task.Run(() => server.Accept());

server.OnClientAccepted += Server_OnClientAccepted;
void Server_OnClientAccepted(Server.Client client)
{
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

    server.SendMessageToClient(recipient, message);
}
void SendMessageToAllClients()
{
    Console.Write("Message: ");
    string message = Console.ReadLine();

    server.SendMessageToAllClients(message);
}

void ConnectedClients()
{
    foreach (var client in server.clients)
    {
        Console.WriteLine("client namae: " + client.name);
        Console.WriteLine("client socket: " + client.socket);
    }
}
void ServerInfo()
{
        Console.WriteLine("Server local address: " + server.localAddress);
        Console.WriteLine("Server socket: " + server.listenSocket);
}




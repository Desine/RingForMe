

Client.Client.server.OnConnectedToServer += () =>
{
    Console.WriteLine("Connected to server");
};
Client.Client.server.receiver.OnRecieved += message => {
    Console.WriteLine("Received message: " + message);
};


while (true)
{
    Console.WriteLine("Options:" +
        "\n1 - Send message" +
        "\n2 - Server info" +
        "\n3 - Change server address" +
        "\n4 - Change server port" +
        "\n5 - Connect to server");

    string choise = Console.ReadLine();
    switch (choise)
    {
        case "1":
            SendMessage();
            break;
        case "2":
            ServerInfo();
            break;
        case "3":
            ChangeServerAddress();
            break;
        case "4":
            ChangeServerPort();
            break;
        case "5":
            ConnectToServer();
            break;
    }
}




void SendMessage()
{
    Console.Write("Message: ");
    string message = Console.ReadLine();

    Client.Client.server.sender.SendMessage(Client.Client.server.socket, message);
}

void ServerInfo()
{
    Console.WriteLine("Server ip end point: "+ Client.Client.server.ipEndPoint);
}

void ChangeServerAddress()
{
    Console.Write("Server adress: ");
    string address = Console.ReadLine();
    Client.Client.server.address = address;
}

void ChangeServerPort()
{
    Console.Write("Server port: ");
    int port = int.Parse(Console.ReadLine());
    Client.Client.server.port = port;
}
void ConnectToServer()
{
    _ = Task.Run(() => Client.Client.server.ConnectToServer(Client.Client.server.ipEndPoint));
}


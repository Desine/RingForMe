

Client.Client client = new();
_ = Task.Run(() => client.ConnectToServer(client.server.ipEndPoint));
client.OnConnectedToServer += () =>
{
    Console.WriteLine("Connected to server");
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

    client.server.sender.SendMessage(client.server.socket, message);
}

void ServerInfo()
{
    Console.WriteLine();
}

void ChangeServerAddress()
{
    Console.Write("Server adress: ");
    string address = Console.ReadLine();
    client.server.address = address;
}

void ChangeServerPort()
{
    Console.Write("Server port: ");
    int port = int.Parse(Console.ReadLine());
    client.server.port = port;
}
void ConnectToServer()
{
    _ = Task.Run(() => client.ConnectToServer(client.server.ipEndPoint));
}




Client.Client client = new();
_ = Task.Run(()=> client.ConnectToServer(client.server.ipEndPoint));



while (true)
{
    Console.WriteLine("Options:" +
        "\n1 - Send message" +
        "\n2 - Serve info");

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
            ChengeServerAdress();
            break;
        case "4":
            ChengeServerPort();
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

void ChengeServerAdress()
{
    Console.Write("Server adress: ");
    string address = Console.ReadLine();
    client.server.address = address;
}

void ChengeServerPort()
{
    Console.Write("Server port: ");
    int port = int.Parse(Console.ReadLine());
    client.server.port = port;
}
void ConnectToServer()
{
    client.ConnectToServer(client.server.ipEndPoint);
}


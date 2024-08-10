

Client.Client client = new();
client.Instantiate();
_ = Task.Run(()=> client.ConnectToServer(client.server.ipEndPoint));



while (true)
{
    Console.WriteLine("Options:\n" +
        "1 - Send message");

    string choise = Console.ReadLine();
    switch (choise)
    {
        case "1":
            SendMessage();
            break;
    }
}




void SendMessage()
{
    Console.Write("Send to: ");
    string recipient = Console.ReadLine();
    Console.Write("Message: ");
    string message = Console.ReadLine();

    client.server.sender.SendMessage(client.server.socket, message);
}





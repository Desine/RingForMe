


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

    server.SendMessageToClient(recipient, message);
}





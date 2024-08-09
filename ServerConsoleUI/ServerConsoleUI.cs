


Server.Server server = new();
server.Instantiate();
_ = Task.Run(() => server.Accept());


while (true)
{
    Console.WriteLine("Options:\b" +
        "1 - Send message");

    int choise = Console.Read();
    switch (choise)
    {
        case 1:
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

    server.SendMessage("me", "Hi, me");

}




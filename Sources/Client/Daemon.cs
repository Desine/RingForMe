

namespace Client;

public static class Daemon
{

    public static Network network = new();


    public static string write = "no";

    private static void Network_OnConnectedToServer()
    {
        write = "connected to server";
    }

    public static void Run()
    {
        network.OnConnectedToServer += Network_OnConnectedToServer;
        network.ConnectToServer();



        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "time.txt");

        while (true)
        {
            File.WriteAllText(filePath, $"{DateTime.Now}\nconnection status: {write}");
            Thread.Sleep(5000);
        }
    }

    public static void ConnectToServer() => network.ConnectToServer();







}

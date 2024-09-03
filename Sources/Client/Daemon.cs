using System;
using System.IO.Pipes;


namespace Client;

public static class Daemon
{


    public static Network network = new();


    public static string write = "default";

    private static void Network_OnConnectedToServer()
    {
        write = "connected to server";
    }

    public static void Run()
    {
        network.OnConnectedToServer += Network_OnConnectedToServer;


        _ = Task.Run(() => PipeMessage());


        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "time.txt");
        while (true)
        {
            File.WriteAllText(filePath, $"{DateTime.Now}\nwrite: {write}");
            Thread.Sleep(5000);
        }
    }



    private static async Task PipeMessage()
    {
        using var namedPipe = new NamedPipeServerStream("rfm.PipeMessage", PipeDirection.In);
        while (true)
        {
            namedPipe.WaitForConnection();

            using var reader = new StreamReader(namedPipe);
            write = reader.ReadLine();
        }
    }




}

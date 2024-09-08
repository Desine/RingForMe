using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.IO.Pipes;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Server;

public static class Daemon
{

    public static Network network = new();

    public static string write = "default";


    public static void Run()
    {


        Write();
    }

    private static async void Write()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "time_server.txt");
        while (true)
        {
            File.WriteAllText(filePath, $"{DateTime.Now}\nwrite: {write}");
            Thread.Sleep(5000);
        }
    }


    private static async Task PipeMessage()
    {
        using var namedPipe = new NamedPipeServerStream("rfm.server.hi", PipeDirection.In);
        while (true)
        {
            namedPipe.WaitForConnection();

            using var reader = new StreamReader(namedPipe);
        }
    }




}

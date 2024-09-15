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


    public static async void RunAsync()
    {
        ReceivePipeMessagesAsync();


        WriteAsync();
    }

    private static async void WriteAsync()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "time_server.txt");
        while (true)
        {
            File.WriteAllText(filePath, $"{DateTime.Now}\nwrite: {write}");
            Thread.Sleep(5000);
        }
    }


    private static async Task ReceivePipeMessagesAsync()
    {
        var tasks = new[]
        {
            HandlePipeMessagesAsync(RfmS.daemonName + ".Hi", OnPipeMessage_Hi),
        };

        await Task.WhenAll(tasks);
    }
    private static async Task HandlePipeMessagesAsync(string pipeName, Action<string> function)
    {
        try
        {
            while (true)
            {
                using var namedPipe = new NamedPipeServerStream(pipeName, PipeDirection.In);

                Console.WriteLine($"Waiting for connection on pipe: {pipeName}");
                await namedPipe.WaitForConnectionAsync();
                Console.WriteLine("Someone connected");

                using var reader = new StreamReader(namedPipe);
                string message;
                while ((message = await reader.ReadLineAsync()) != null)
                {
                    Console.WriteLine($"Received on {pipeName}: {message}");
                    function(message);
                }

                // After the client disconnects, NamedPipeServerStream needs to be reset for the next connection
                namedPipe.Disconnect();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in {nameof(HandlePipeMessagesAsync)}: {ex.Message}");
        }
    }

    private static void OnPipeMessage_Hi(string message)
    {
        write = message;
    }




}

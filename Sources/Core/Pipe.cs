using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;


namespace Core;


public static class Pipe
{
    public static void SendMessage(string pipeName, string message)
    {
        using var namedPipe = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);

        namedPipe.Connect();

        using var writer = new StreamWriter(namedPipe);

        writer.WriteLine(message);
        writer.Flush();
    }


    public static async Task ReceiveFunctionMessagesAsync(string pipeName, Action<string> function)
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

            namedPipe.Disconnect();
        }
    }

}
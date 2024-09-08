using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.IO.Pipes;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace Server;

public static class RfmS
{


    public const string daemonName = "rfms";


    public struct Command{
        public string name;
        public string description;
        public Func<string, string> function;
    }
    public static List<Command> commands = new(){
        new Command
        {
            name = "help",
            description = "Displays help menu",
            function = (input) => {
            string output = "It's help menu. What do you want?";
            foreach(var command in commands){
                output += "\n   " + command.name + "   " + command.description;
            }            
            return output;
            }
        },
    };



    public static void Main(string[] args)
    {

        if (args.Length == 0) TryRunDaemon();
        else
        {
            switch (args[0])
            {
                case "daemon":
                    {
                        Daemon.Run();
                        break;
                    }
            }
        }
    }







    private static void TryRunDaemon()
    {
        if (IsDaemonAlive()) Console.WriteLine("rfms Daemon is already alive");
        else
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = Environment.ProcessPath,
                Arguments = $"daemon &",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            });
        }
    }

    public static bool IsDaemonAlive()
    {
        return Process.GetProcessesByName(daemonName).Length > 1;
    }
    public static void KillDaemon()
    {
        foreach (Process process in Process.GetProcessesByName(daemonName))
        {
            if (process == Process.GetCurrentProcess()) continue;

            try
            {
                process.Kill();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error killing process: " + ex.Message);
            }
            finally
            {
                process.Close();
            }
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

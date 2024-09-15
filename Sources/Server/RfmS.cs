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


    public struct Command
    {
        public string name;
        public string[] args;
        public string description;
        public Func<string[], string> function;
    }
    public static List<Command> commands = new(){
        new Command
        {
            name = "\" \"",
            description = "No args - run a daemon in background",
            function = (args) => {
            string output = "Daemon was alive: " + IsDaemonAlive();
            TryRunDaemon();
            output += "\nDaemon is alive: " + IsDaemonAlive();
            return output;
            }
        },
        new Command
        {
            name = "help",
            description = "Displays help menu",
            function = (args) => {
            string output = "It's help menu. What do you want?";
            foreach(var command in commands){
                output += "\n   " + command.name + "   " + command.description;
            }
            return output;
            }
        },
        new Command
        {
            name = "daemon",
            description = "Runs as daemon",
            function = (args) => {
            string output = "";
            Daemon.RunAsync();
            return output;
            }
        },
        new Command
        {
            name = "daemon-kill",
            description = "Kills the daemon",
            function = (args) => {
            string output = "";
            KillDaemon();
            return output;
            }
        },
        new Command
        {
            name = "daemon-message",
            description = "Send message to the daemon via named pipe",
            function = (args) => {
            string output = "";
            if(args.Length > 1) SendPipeMessage(daemonName + "." + args[0], string.Join(' ', args.Skip(1)));
            return output;
            }
        },
    };


    public static void Main(string[] args)
    {
        string[] argsForFunc = args.Skip(1).ToArray();
        string commandName = args.Length > 0 ? args[0] : "";
        commands.ForEach(command =>
        {
            if (command.name == commandName) Console.WriteLine(command.function(argsForFunc));
        });
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
            process.Kill();
        }
    }



}

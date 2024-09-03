using System.Diagnostics;
using System.IO.Pipes;


namespace Client;

public static class Rfm
{

    public const string daemonName = "rfm";



    public static async Task Main(string[] args)
    {

        if (args.Length == 0) TryRunDaemon();
        else {
            switch (args[0])
            {
                case "daemon":
                    Daemon.Run();
                    break;
                case "frds":
                    Console.WriteLine("You have no friends");
                    break;
                case "con":
                    Console.WriteLine("Connecting to server");
                    break;
                case "killd":
                    KillDaemon();
                    break;
                case "msg":
                    if(args.Length < 2) Console.WriteLine("message contence?");
                    else PipeMessage(args[1]);
                    break;
                default:
                    Console.WriteLine("default: args[0] = :" + args[0]);
                    break;
            }
        }
    }

    private static void TryRunDaemon()
    {
        if (IsDaemonAlive()) Console.WriteLine("rfm Daemon is already alive");
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

    private static void PipeMessage(string message)
    {
        using var namedPipe = new NamedPipeClientStream(".", "rfm.PipeMessage", PipeDirection.Out);
        namedPipe.Connect();

        using var writer = new StreamWriter(namedPipe);
        writer.Write(message);
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


}
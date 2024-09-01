using System.Diagnostics;
using Tmds.DBus.Protocol;


namespace Client;

public static class Rfm
{

    public const string daemonName = "rfm";



    public static void Main(string[] args)
    {

        if (args.Length == 0)
        {
            if (IsDaemonAlive() == false)
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
        else if (args[0] == "daemon")
        {
            Daemon.Run();
        }
        else
        {
            switch (args[0])
            {
                case "frds":
                    Console.WriteLine("You have no friends");
                    break;
                case "con":
                    Console.WriteLine("Connecting to server");
                    break;
                case "killd":
                    KillDaemon();
                    break;
                default:
                    Console.WriteLine("default: args[0] = :" + args[0]);
                    break;
            }
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


}
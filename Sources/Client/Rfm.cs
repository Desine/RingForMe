using System.Diagnostics;


namespace Client;

public static class Rfm
{





    public static void Main(string[] args)
    {

        if (args.Length == 0)
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
                default:
                    Console.WriteLine("default");
                    break;
            }
        }
    }
}
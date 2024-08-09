using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core;

internal class Sender
{

    static async Task SendMessagesAsync(Socket socket, string message)
    {
        byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
        await socket.SendAsync(sendBuffer, SocketFlags.None);
    }


}

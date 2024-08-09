using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core;

public class Sender
{

    public async Task SendMessageAsync(Socket socket, string message)
    {
        byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
        await socket.SendAsync(sendBuffer, SocketFlags.None);
    }


}

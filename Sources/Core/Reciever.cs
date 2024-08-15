using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Core;


public class Receiver
{


    public Action<string> OnReceived = _ => {};


    public async Task ReceiveMessageAsync(Socket socket)
    {
        while (true)
        {
            byte[] receiveBuffer = new byte[1024];
            int receiveLength = await socket.ReceiveAsync(receiveBuffer, SocketFlags.None);

            if (receiveLength > 0)
            {
                string message = Encoding.ASCII.GetString(receiveBuffer, 0, receiveLength);
                OnReceived(message);
            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Core;


public class Reciever
{


    public Action<WebDataProtocol.Message> OnRecieved = _ => {};


    async Task Recieve(Socket socket)
    {
        while (true)
        {
            byte[] receiveBuffer = new byte[1024];
            int receiveLength = await socket.ReceiveAsync(receiveBuffer, SocketFlags.None);

            if (receiveLength > 0)
            {
                string message = Encoding.ASCII.GetString(receiveBuffer, 0, receiveLength);
                OnRecieved(JsonSerializer.Deserialize<WebDataProtocol.Message>(message));
            }
        }
    }


}

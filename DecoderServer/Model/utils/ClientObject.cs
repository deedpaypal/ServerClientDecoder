using System;
using System.Net.Sockets;
using System.Text;
using DecoderServer.Interfaces;
using DecoderServer.Model.Entities;

namespace DecoderServer.Model.utils
{
    public class ClientObject: IClientObject
    {
        public TcpClient client;
        public ClientObject(TcpClient tcpClient)
        {
            client = tcpClient;
        }

        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64]; // буфер для получаемых данных
                while (true)
                {
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;

                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    Console.WriteLine(message);

                    if (message.Length > 2)
                    {
                        //шифруем
                        DecoderHandler decoder = new DecoderHandler(message.Remove(0, 2));
                        ;

                        if (message.StartsWith("d:"))
                        {

                            message = decoder.EncrypteString();
                        }
                        else if (message.StartsWith("e:"))
                        {
                            message = decoder.DecrypteString();
                        }
                        else
                        {
                            message = "Incorrect!";
                        }
                    }
                    else
                    {
                        message = "Incorrect!";
                    }

                    // отправляем обратно
                    data = Encoding.Unicode.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
            }
       
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
        }
    }
}

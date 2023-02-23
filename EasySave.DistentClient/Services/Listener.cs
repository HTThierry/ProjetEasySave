using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EasySave.DistentClient.Services
{
    internal class Listener
    {
        public int Port { get; set; }
        public EventHandler<msgEventArgs> msgRecevie;
        public Object Locker;

        public Listener(int port)
        {
            Port = port;
        }

        public void Start()
        {
            Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port));
            Debug.WriteLine("Chat connecté au serveur");

            new Thread(Listen).Start(socket);
        }

        public void Listen(object? obj)
        {
            Socket? socket = obj as Socket;
            if (socket == null)
                return;

            byte[] buffer = new byte[4096];

            while (true)
            {
                int length = socket.Receive(buffer);
                string message = System.Text.Encoding.UTF8.GetString(buffer, 0, length);
                msgRecevie?.Invoke(this, new msgEventArgs() { msg = message });
            }
        }

        public class msgEventArgs : EventArgs
        {
            public string? msg { get; set; }
        }
    }
}
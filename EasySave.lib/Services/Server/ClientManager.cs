using EasySave.lib.Models;
using System.Net.Sockets;
using System.Text.Json;

namespace EasySave.lib.Services.Server
{
    public class ClientManager
    {
        public event EventHandler? Disconnected;

        public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

        public Socket Socket { get; set; }

        public ClientManager(Socket socket)
        {
            Socket = socket;
        }

        public void Start()
        {
            new Thread(Listen).Start();
            Send("Hello");
        }

        public void Send(string message)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
            Socket.Send(buffer);
        }

        //public void Send(List<SaveWorkModel> saveWorkModels)
        //{
        //    string message = JsonSerializer.Serialize(saveWorkModels);
        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
        //    Socket.Send(buffer);
        //}

        private void Listen()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int length = Socket.Receive(buffer);
                    string message = System.Text.Encoding.UTF8.GetString(buffer, 0, length);
                    Console.WriteLine(message);

                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs() { Message = message });
                }
            }
            catch
            {
                Disconnected?.Invoke(this, EventArgs.Empty);
                Socket.Dispose();
            }
        }
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; set; } = "";
    }
}
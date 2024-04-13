using City.MainWindowClasses;
using City.Properties;
using ClassesLibrary.Client;
using ClassesLibrary.SystemInfo;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace City.Models
{
    class ShellModel
    {
        private UdpClient _udpClient = new UdpClient();
        private static string _ip = "127.0.0.1";
        private static int _connctionPort = 5554;
        private static IPEndPoint _connctionEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _connctionPort);
        private UdpReceiveResult _receiveMessageResult;
        public ShellModel(string id)
        {
            SendId(id);
            Task.Run(() =>
            {
                GetMessages();
            });
        }

        private void SendId(string id)
        {
            var data = Encoding.UTF8.GetBytes($"ServerName {id}");
            try
            {
                _udpClient.Send(data, data.Length, _connctionEndPoint);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task GetMessages()
        {
            while (true)
            {
                try
                {
                    _receiveMessageResult = await _udpClient.ReceiveAsync();
                    if (_receiveMessageResult.Buffer != null)
                    {
                        var receivedData = Encoding.UTF8.GetString(_receiveMessageResult.Buffer);
                        if (receivedData.Contains("Ready"))
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
            Task.Run(() =>
            {
                while (true)
                {
                    SendDataToClient();
                    Thread.Sleep(1000);
                }
            });
        }

        private async Task SendDataToClient()
        {
            byte[] data;
            if (!LaptopCheck.IsPcLaptop())
                data = Encoding.UTF8.GetBytes(CreateJson.Create(SystemInfo.GetNotebookBatary()));
            else
                data = Encoding.UTF8.GetBytes(CreateJson.Create(""));
            try
            {
                await _udpClient.SendAsync(data, data.Length, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
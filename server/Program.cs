using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using ConfigMaster;
using static Preteer.Painter.Style;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string multicastAddress = Configer.GetValue("config.xml", "/root/address");
            int portValue = int.Parse(Configer.GetValue("config.xml", "/root/port"));

            using (UdpClient udpClient = new UdpClient())
            {
                udpClient.JoinMulticastGroup(IPAddress.Parse(multicastAddress), 50);

                string message = null;
                while (message!="server/stop") {
                Wait("Введите сообщение для отправки:");
                message = Console.ReadLine();

                byte[] data = Encoding.UTF8.GetBytes(message);

                udpClient.Send(data, data.Length, multicastAddress, portValue);

                Success("Сообщение отправлено.");
                }
            }
        }
    }
}

using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using ConfigMaster;
using static Preteer.Painter.Style;

namespace client
{
    internal class Program
    {


        static void Main(string[] args)
        {
            string multicastAddress = Configer.GetValue("config.xml", "/root/address");
            int portValue = int.Parse(Configer.GetValue("config.xml", "/root/port"));

            UdpClient udpClient = new UdpClient(portValue);

            // Присоединяемся к мультикаст-группе с указанным адресом
            udpClient.JoinMulticastGroup(IPAddress.Parse(multicastAddress));
            SystemMessage($"Прием сообщений на мультикаст-адресе {multicastAddress}:{portValue}");

            try
            {
                while (true)
                {
                    // Принимаем данные от мультикаст-группы
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, portValue);
                    byte[] receivedData = udpClient.Receive(ref remoteEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedData);

                    Message($"Получено сообщение от {remoteEndPoint}: {receivedMessage}");
                }
            }
            catch (Exception ex)
            {
                Error($"Ошибка приема сообщений: {ex.Message}");
            }
            finally
            {
                udpClient.Close();
            }
        }
    }
}

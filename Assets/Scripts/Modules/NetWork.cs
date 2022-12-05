using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Modules
{
    public class NetWork
    {
        public static class NetWorkGet 
        {
            public static Socket udpSocket;
            public static IPEndPoint localIp;
            public static EndPoint remoteIp;
            public static TcpListener tcpListener;


            public static string GetIP()
            {
                string host = Dns.GetHostName();
                IPAddress address = Dns.GetHostAddresses(host).First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                return localIp.Address.ToString();

            }
            public static string UdpInitServer(int port, ref string host)
            {
                host = Dns.GetHostName();
                IPAddress address = Dns.GetHostAddresses(host).First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                localIp = new IPEndPoint(address, port);
                udpSocket.Bind(localIp);
                remoteIp = new IPEndPoint(IPAddress.Any, 0);
                return localIp.Address.ToString();
            }
            public static string UdpOpenPort(int port)
            {
                string host = Dns.GetHostName();
                IPAddress address = Dns.GetHostAddresses(host).First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                localIp = new IPEndPoint(address, port);
                udpSocket.Bind(localIp);
                remoteIp = new IPEndPoint(IPAddress.Any, 0);
                return localIp.Address.ToString();
            }
            public static async Task<string> UdpGetMessage()
            {
                byte[] buffer = new byte[256];

                while (true)
                {
                    var result = await udpSocket.ReceiveFromAsync(buffer, SocketFlags.None, remoteIp);
                    string message = Encoding.UTF8.GetString(buffer, 0, result.ReceivedBytes);
                    return message;
                }
            }

            public static void TcpInitServer(int port)
            {
                tcpListener = new TcpListener(IPAddress.Any, port);
            }
            public static async Task TcpGetMessage()
            {

                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений... ");

                while (true)
                {
                    using var tcpClient = await tcpListener.AcceptTcpClientAsync();
                    var stream = tcpClient.GetStream();
                    var response = new List<byte>();
                    int bytesRead = 10;
                    while ((bytesRead = stream.ReadByte()) != '\n')
                    {
                        response.Add((byte)bytesRead);
                    }
                    var request = Encoding.UTF8.GetString(response.ToArray());
                    if (request == "END") break;

                    Console.WriteLine($"Запрос {request}");
                    response.Clear();
                    string result = "result" + "\n";
                    Console.WriteLine($"Ответ {result}");
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(result));

                }
            }

        }
        public class NetWorkSend {
            private EndPoint remotePoint;
            private Socket udpSocket;
            private TcpClient tcpClient;
            private string ip;
            private int port;
            public void SetEndPoint(string ipAddressRemote, int port)
            {
                remotePoint = new IPEndPoint(IPAddress.Parse(ipAddressRemote), port);
                ip = ipAddressRemote;
                this.port = port;
            }
            public void UdpConnect()
            {
                udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }
            public async Task UdpSend(string message)
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                int bytes = await udpSocket.SendToAsync(data, SocketFlags.None, remotePoint);
            }
            public async Task TcpConnect()
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(ip, port);
            }
            public async Task TcpSend(string message)
            {
                // слова для отправки для получения перевода
                var words = new string[] { "red", "yellow", "blue" };
                // получаем NetworkStream для взаимодействия с сервером
                var stream = tcpClient.GetStream();

                // буфер для входящих данных
                var response = new List<byte>();
                int bytesRead = 10; // для считывания байтов из потока
                                    // считыванием строку в массив байт
                                    // при отправке добавляем маркер завершения сообщения - \n
                byte[] data = Encoding.UTF8.GetBytes(message + '\n');
                // отправляем данные
                await stream.WriteAsync(data);

                // считываем данные до конечного символа
                while ((bytesRead = stream.ReadByte()) != '\n')
                {
                    // добавляем в буфер
                    response.Add((byte)bytesRead);
                }
                var translation = Encoding.UTF8.GetString(response.ToArray());
                Console.WriteLine($"Слово {message}: {translation}");
                response.Clear();

                // отправляем маркер завершения подключения - END
                await stream.WriteAsync(Encoding.UTF8.GetBytes("END\n"));
                Console.WriteLine("Все сообщения отправлены");
            }
            public async void Requst(string message)
            {
                TcpConnect().Wait();
                await TcpSend(message);
            }
        }
    }
}

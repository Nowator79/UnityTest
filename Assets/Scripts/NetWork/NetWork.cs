using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Modules
{
    public class NetWork
    {
        public const string CloseCommand = "CLOSE";
        public static class NetWorkGet
        {
            public static Socket udpSocket;
            public static Socket tcpSocketServer;
            public static IPEndPoint localIp;
            public static EndPoint remoteIp;

            public static string GetIP()
            {
                string host = Dns.GetHostName();
                IPAddress address = Dns.GetHostAddresses(host).First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                return localIp.Address.ToString();
            }
            public static string GetHostName()
            {
                return Dns.GetHostName();
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
            public static void UdpClosePort()
            {
                udpSocket.Close();
                udpSocket = null;
            }
            public static async Task<string> UdpGetMessage()
            {
                byte[] buffer = new byte[256];
                        
                string message = "";
                while (true)
                {
                    if (udpSocket != null)
                    {
                        try
                        {
                            SocketReceiveFromResult result = await udpSocket.ReceiveFromAsync(buffer, SocketFlags.None, remoteIp);
                            message = Encoding.UTF8.GetString(buffer, 0, result.ReceivedBytes);
                        }
                        catch
                        {
                            break;
                        }
                    }
                    return message;
                }
                return message;

            }

            public static void TcpInitServer(int port)
            {
                tcpSocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tcpSocketServer.Bind(new IPEndPoint(IPAddress.Any, port));
                tcpSocketServer.Listen(0);

            }
            public static void TcpCloseServer()
            {
                tcpSocketServer.Close();
            }
           

            public static async Task TcpListenMessage(Action<string, StringResult, string> responseHandler)
            {

                Socket tcpClient = await tcpSocketServer.AcceptAsync();
                _ = Task.Run(async () => await process(tcpClient, responseHandler));

               
              

                static async Task process(Socket tcpClient, Action<string, StringResult, string> responseHandler)
                {
                 
                    string ipAddress = ((IPEndPoint)tcpClient.RemoteEndPoint).Address.ToString();

                    // буфер для накопления входящих данных
                    var response = new List<byte>();
                    // буфер для считывания одного байта
                    var bytesRead = new byte[1];
                    // считываем данные до конечного символа
                    while (true)
                    {
                        var count = await tcpClient.ReceiveAsync(bytesRead, SocketFlags.None);
                        // смотрим, если считанный байт представляет конечный символ, выходим
                        if (count == 0 || bytesRead[0] == '\n') break;
                        // иначе добавляем в буфер
                        response.Add(bytesRead[0]);
                    }

                    string request = Encoding.UTF8.GetString(response.ToArray());


                    response.Clear();
                    string result = "";
                    StringResult stringResult = new(result);

                    responseHandler(request, stringResult, ipAddress);
                    stringResult.str += "\n";
                    await tcpClient.SendAsync(Encoding.UTF8.GetBytes(stringResult.str), SocketFlags.None);
                }

            }
            public class StringResult
            {
                public string str = "";
                public StringResult(string str)
                {
                    this.str = str;
                }
                     
            }
        }
        public class NetWorkSend
        {
            private EndPoint remotePoint;
            private Socket udpSocket;
            private TcpClient tcpClient;
            public string IP { get; private set; }
            public int port;

            public void SetEndPoint(string ipAddressRemote, int port)
            {
                remotePoint = new IPEndPoint(IPAddress.Parse(ipAddressRemote), port);
                IP = ipAddressRemote;
                this.port = port;
            }
            public void UdpConnect()
            {
                udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }
            public async void UdpSend(string message)
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                _ = await udpSocket.SendToAsync(data, SocketFlags.None, remotePoint);
            }
            public void TcpConnect()
            {
                tcpClient = new TcpClient();
                tcpClient.ConnectAsync(IP, port).Wait();
            }
            public async Task<string> TcpSend(string message)
            {
                NetworkStream stream = tcpClient.GetStream();

                List<byte> response = new();
                byte[] data = Encoding.UTF8.GetBytes(message + '\n');
                await stream.WriteAsync(data);

                int bytesRead;
                while ((bytesRead = stream.ReadByte()) != '\n')
                {
                    response.Add((byte)bytesRead);
                }

                string result = Encoding.UTF8.GetString(response.ToArray());
                response.Clear();

                return result;
            }
            public async Task TcpRequst(string message)
            {
                await Task.Run(() =>
                {
                    TcpConnect();
                    Task sendTask = TcpSend(message);
                    sendTask.Wait();
                });
            }
            public async Task TcpRequst(string message, Action<string> handlerResult)
            {
                await Task.Run(async () =>
                {
                    TcpConnect();
                    string result = await TcpSend(message);
                    await Task.Run(() => handlerResult(result));
                });
            }
        }
    }
}

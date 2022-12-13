using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
         
            public static async Task TcpListenMessage(Action<string, StringResult, string> responseHandler)
            {
                tcpListener.Start();

                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                string ipAddress = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
                NetworkStream stream = tcpClient.GetStream();
                List<byte> response = new();

                int bytesRead = 10;
                while ((bytesRead = stream.ReadByte()) != '\n')
                {
                    response.Add((byte)bytesRead);
                }
                string request = Encoding.UTF8.GetString(response.ToArray());
                response.Clear();
                string result = "";
                StringResult stringResult = new StringResult(result);
                responseHandler(request, stringResult, ipAddress);
                stringResult.str += "\n";
                await stream.WriteAsync(Encoding.UTF8.GetBytes(stringResult.str));
            }
            public class StringResult
            {
                public string str = "";
                public StringResult(string str)
                {
                    this.str = str;
                }
                     
            }
            public static void Disconected()
            {
                try
                {
                    tcpListener.Stop();
                }
                catch
                {
                }
            }
        }
        public class NetWorkSend
        {
            private EndPoint remotePoint;
            private Socket udpSocket;
            private TcpClient tcpClient;
            public string ip { get; private set; }
            public int port;

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
                Debug.Log($"send: udp,  remotePoint: {ip}");
                byte[] data = Encoding.UTF8.GetBytes(message);
                int bytes = await udpSocket.SendToAsync(data, SocketFlags.None, remotePoint);
            }
            public void TcpConnect()
            {
                tcpClient = new TcpClient();
                tcpClient.ConnectAsync(ip, port).Wait();
            }
            public async Task<string> TcpSend(string message)
            {
                NetworkStream stream = tcpClient.GetStream();

                List<byte> response = new();
                int bytesRead = 10;
                byte[] data = Encoding.UTF8.GetBytes(message + '\n');
                await stream.WriteAsync(data);

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

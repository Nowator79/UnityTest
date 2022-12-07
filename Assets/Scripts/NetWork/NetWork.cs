using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
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
            public static async Task<NetworkStream> TcpGetStream()
            {

                tcpListener.Start();

                while (true)
                {
                    using TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                    NetworkStream stream = tcpClient.GetStream();
                    return stream;
                }
            }
            public static async void TcpWriteResult(string result, NetworkStream stream)
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes(result));
            }

        }
        public class NetWorkSend
        {
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
            public async Task<string> TcpSend(string message)
            {
                var stream = tcpClient.GetStream();
                var response = new List<byte>();
                int bytesRead = 10;
                byte[] data = Encoding.UTF8.GetBytes(message + '\n');
                await stream.WriteAsync(data);
                while ((bytesRead = stream.ReadByte()) != '\n')
                {
                    response.Add((byte)bytesRead);
                }
                var translation = Encoding.UTF8.GetString(response.ToArray());
                response.Clear();

                await stream.WriteAsync(Encoding.UTF8.GetBytes("END\n"));
                return translation;
            }
            public async void Requst(string message)
            {
                TcpConnect().Wait();
                await TcpSend(message);
            }
        }
    }
}

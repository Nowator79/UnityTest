using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Modules
{

    public class NetWork
    {
        public static class UdpNetWork 
        {
            private static string ip = "192.168.0.10";
            private static int port = 3002;
            public static Socket udpSocket;
            public static IPEndPoint localIp;
            public static EndPoint remoteIp;

            public static void InitServer()
            {
                udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                localIp = new IPEndPoint(IPAddress.Parse(ip), port);
                udpSocket.Bind(localIp);
                remoteIp = new IPEndPoint(IPAddress.Any, 0);
            }
            public static async Task<string> GetMessage()
            {
                byte[] buffer = new byte[256];

                while (true)
                {
                    var result = await udpSocket.ReceiveFromAsync(buffer, SocketFlags.None, remoteIp);
                    string message = Encoding.UTF8.GetString(buffer, 0, result.ReceivedBytes);
                    return message;
                }
            }
        }
    }
}

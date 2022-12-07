using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using static Scripts.Modules.NetWork;

namespace Scripts
{
    public class NetWorkMB : UIBase
    {
        public static bool IsClient = true;
        private const int PortServer = 3002;
        [SerializeField]
        private bool IsConnectd = false;

        [SerializeField]
        private UINetWorkForm uINetWorkForm;

        private NetWorkSend addressServer;

        private List<NetWorkSend> playersNetWork = new();

        public static NetWorkMB StaticNetWorkMB;
        private NetWorkMB()
        {
            StaticNetWorkMB = this;
        }
   
        public async void ConnectToServer(string IPAddres, int Port)
        {
            Task listenHandler = Listen(PortServer);
            bool SuccessfulConnect =  await Connect(IPAddres, Port);
            if (SuccessfulConnect)
            {
                UIDebug.Log($"Succesful connect to {IPAddres}:{Port}");
            }
            else
            {
                UIDebug.Log($"Unsuccesful connect to {IPAddres}:{Port}");
                uINetWorkForm.gameObject.SetActive(true);
                listenHandler.Dispose();
            }
        }
        public void Disconnect()
        {
            IsConnectd = false;
        }
        public void StartServer()
        {
            //Открывем порт для получения данных
            //Асинхронный цикл получения данных
            IsClient = false;
            _ = Listen(PortServer);
            GameStatus gameStatus = GameStatus.StaticGameStatus;
            gameStatus.StartGame();
            UIDebug.Log($"Is server");
        }
        private async Task Listen(int port)
        {
            Task listenUdp = ListenUdp(port);
            Task listenTcp = ListenTcp(port);
        }
        private async Task ListenUdp(int port)
        {
            NetWorkGet.UdpOpenPort(port);

            while (true)
            {
                string command = await NetWorkGet.UdpGetMessage();
                CommandRout(command);
            }
        }
        private async Task ListenTcp(int port)
        {
            NetWorkGet.TcpInitServer(port);

            while (true)
            {
                NetworkStream stream = await NetWorkGet.TcpGetStream();
                UIDebug.Log("Connect");
                //CommandRout(command);
            }
            async Task GetMessage()
            {

            }
        }
        private async Task<bool> Connect(string IPAddres, int Port)
        {
            //Соединение для отправки сообщений
            //Куда будут слаться сообщения
            addressServer = new();
            addressServer.SetEndPoint(IPAddres, Port);
            addressServer.UdpConnect();
            addressServer.TcpConnect();
            UIDebug.Log(await addressServer.TcpSend("connect"));

            string localIp = NetWorkGet.GetIP();
            //Посылаем пакет данных показывающий подсоединение для сервера.(Переделать в угоду безопастности)
            CommandTemplate template = new()
            {
                TypeCommandStr = "Connect",
                CustomStrData = localIp
            };

            SendCommand(addressServer, template);
            //Ждем ответ от сервера в течение 5 сек
            for (int i = 0; i < 5; i++)
            {
                if (IsConnectd)
                {
                    break;
                }
                await Task.Delay(1000);
            }
          
            return IsConnectd;
        }
        private void CommandRout(string command)
        {
            CommandTemplate myObject = JsonConvert.DeserializeObject<CommandTemplate>(command);
            Debug.Log(command);

            if (myObject.TypeCommandStr != "")
            {
                //команды которые надо использовать
                switch (myObject.TypeCommandStr)
                {
                    case "Connect":
                        UIDebug.Log($"Player connected: {myObject.CustomStrData}");
                        CommandTemplate SuccessfulConnect = new()
                        {
                            TypeCommandStr = "SuccesfulConnect",
                        };
                        NetWorkSend player = new();
                        player.SetEndPoint(myObject.CustomStrData, PortServer);
                        player.UdpConnect();
                        playersNetWork.Add(player);
                        SendCommand(player, SuccessfulConnect);
                        break;
                    case "SuccesfulConnect":
                        IsConnectd = true;
                        break;
                    case "ping":
                        UIDebug.Log($"Ping");
                        break;
                }
            }
        }
        public void SendCommand(NetWorkSend remoteServer, CommandTemplate typeCommand )
        {
            Debug.Log(typeCommand);
            Task task = remoteServer.UdpSend(typeCommand.ToString());
        }
        private async Task PingUsers()
        {
            while (true)
            {
                foreach (NetWorkSend player in playersNetWork)
                {
                    await player.UdpSend(CommandTemplate.PingCommand.ToString());
                }
                await Task.Delay(1000);
            }
        }
    }
}

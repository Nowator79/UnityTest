using Newtonsoft.Json;
using System.Collections.Generic;
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
        public async void Disconnect()
        {
            IsConnectd = false;
        }
        public void StartServer()
        {
            //�������� ���� ��� ��������� ������
            //����������� ���� ��������� ������
            IsClient = false;
            _ = Listen(PortServer);
            _ = PingUsers();
            GameStatus gameStatus = GameStatus.Get();
            gameStatus.StartGame();
            UIDebug.Log($"Is server");
        }
        private async Task Listen(int port)
        {
            NetWorkGet.UdpOpenPort(port);

            while (true)
            {
                string command = await NetWorkGet.UdpGetMessage();
                CommandRout(command);
            }
        }

        private async Task<bool> Connect(string IPAddres, int Port)
        {
            //���������� ��� �������� ���������
            //���� ����� ������� ���������
            addressServer = new();
            addressServer.SetEndPoint(IPAddres, Port);
            addressServer.UdpConnect();
            string localIp = NetWorkGet.GetIP();
            //�������� ����� ������ ������������ ������������� ��� �������.(���������� � ����� �������������)
            CommandTemplate template = new()
            {
                TypeCommandStr = "Connect",
                CustomStrData = localIp
            };

            SendCommand(addressServer, template);
            //���� ����� �� ������� � ������� 5 ���
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
                //������� ������� ���� ������������
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

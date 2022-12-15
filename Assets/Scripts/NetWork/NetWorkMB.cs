using NetWork.TypeJsonBody;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Scripts.Modules.NetWork;

namespace Scripts
{
    public class NetWorkMB : MonoBehaviour
    {
        public static bool IsClient = true;
        public const int PortServer = 5025;
        public Task connectListenTcp;
        public Task tcpListenMessage;
        public Task udpClientListenMessage;
        public Task syncPosition;
        private bool ServerIsStart = false;
        public ClientStatus ClientStatus = new();
        private NetWorkSend addressServer;
        public static NetWorkMB StaticNetWorkMB;
        public List<NetWorkSend> UdpListClients { get; set; } = new();
        private NetWorkMB()
        {
            StaticNetWorkMB = this;
        }
        private void Start()
        {
            CommendRouting.IncludeCommands();
        }
    
        public async void ConnectToServer(string IPAddres, int Port)
        {
            bool SuccessfulConnect = await Connect(IPAddres, Port);
            if (SuccessfulConnect)
            {
                UIDebug.Log($"Succesful connect to {IPAddres}:{Port}");
                await SendRequst("GetOnline", true);
                await SendRequst("GetWorldObject", true);
                GameWorld.StaticGameWorld.FindUnitById(GameStatus.StaticGameStatus.PlayerId).SetCamera();
                udpClientListenMessage = ListenUdp(Port);
                GameStatus.StaticGameStatus.StartGameClient();
            }
            else
            {
                UIDebug.Log($"Unsuccesful connect to {IPAddres}:{Port}");
            }
        }
        public async void Disconnect()
        {
            ServerIsStart = false;
            NetWorkGet.Disconected();
            await SendRequst("Disconected");
            GameWorld.StaticGameWorld.Destroy();
        }
        public void StartServer()
        {
            IsClient = false;
            Listen(PortServer);
            GameStatus gameStatus = GameStatus.StaticGameStatus;
            gameStatus.StartGameServer();
            UIDebug.Log("Start server");
            NetWorkPlayers.StaticNetWorkPlayers.Add(GameStatus.StaticGameStatus.PlayerName);
            InvokeRepeating(nameof(SyncPositionInvoke), 0, 0.02f);
        }
        private void Listen(int port)
        {
            ServerIsStart = true;
            //Task listenUdp = ListenUdp(port);
            connectListenTcp = ListenTcp(port);
        }
        private async Task ListenUdp(int port)
        {
            try
            {
                NetWorkGet.UdpOpenPort(port);

                while (true)
                {
                    string command = await NetWorkGet.UdpGetMessage();
                    CommendRouting.CommandRout(command, "udp", "");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }

        }
        private async Task ListenTcp(int port)
        {
            NetWorkGet.TcpInitServer(port);

            while (true)
            {

                tcpListenMessage = NetWorkGet.TcpListenMessage(
                    (string responce, NetWorkGet.StringResult result, string ipAddress) =>
                    {
                        result.str = CommendRouting.CommandRout(responce, "tcp", ipAddress);
                    }
                );
                await tcpListenMessage;
                if (!ServerIsStart)
                {
                    UIDebug.Log("Close server");
                    break;
                }
            }
        }
        private async Task<bool> Connect(string IPAddres, int Port)
        {
            addressServer = new();
            addressServer.SetEndPoint(IPAddres, Port);

            await SendRequst("TryConnect", true);

            int WaitSecunds = 5;
            for (int i = 0; i < WaitSecunds; i++)
            {
                if (ClientStatus.IsWait)
                {
                    continue;
                }
                if (ClientStatus.IsConnect)
                {
                    return true;
                }
                if (ClientStatus.IsDisconnect)
                {
                    return false;
                }

                Task.Delay(1000).Wait();
            }
            ClientStatus.ConnectUnSuccessful();
            return false;
        }
        public void SendCommand(NetWorkSend remoteServer, CommandTemplate typeCommand)
        {
            Debug.Log(typeCommand.ToString());
            remoteServer.UdpSend(typeCommand.ToString());
        }
        public void SetNameToCommend(ref CommandTemplate command)
        {
            command.UserName = GameStatus.StaticGameStatus.PlayerName;
            command.Id = GameStatus.StaticGameStatus.PlayerId;
        }
        public async Task SendRequst(string TypeCommand, bool responce = false)
        {
            CommandTemplate tryConnect = new()
            {
                TypeCommandStr = TypeCommand,
            };

            SetNameToCommend(ref tryConnect);

            if (responce)
            {
                await addressServer.TcpRequst(
                    tryConnect.ToString(),
                    (result) =>
                    {
                        NetWorkResult netWorkResult = new(result);
                        netWorkResult.SetTcp();
                        QureyReader.StaticQureyReader.SetProcessing(netWorkResult);
                    }
                );
            }
            else
            {
                await addressServer.TcpRequst(
                  tryConnect.ToString()
              );
            }
        }
        /// <summary>
        /// �������� ������� �������
        /// </summary>
        /// <param name="TypeCommand"></param>
        /// <param name="responce"></param>
        /// <returns></returns>
        public Task SendRequst(CommandTemplate TypeCommand, bool responce = false)
        {
            return SendRequst(TypeCommand.TypeCommandStr, responce);
        }

        private void SyncPositionInvoke()
        {
            foreach (NetWorkSend player in UdpListClients)
            {
                World world = GameWorld.StaticGameWorld.GetWorld();
                foreach (NetWork.TypeJsonBody.GameObject element in world.objects)
                {
                    try
                    {
                        CommandTemplate command = new() { TypeCommandStr = "MoveWorldObject" };
                        command.SetJsonBody(element);
                        player.UdpSend(command.ToString());
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
        }
    }
}

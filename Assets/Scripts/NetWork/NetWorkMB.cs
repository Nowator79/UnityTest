using NetWork.TypeJsonBody;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using static Scripts.Modules.NetWork;

namespace Scripts
{
    public class NetWorkMB : MonoBehaviour
    {
        public static bool IsClient { get; set; } = true;
        public const int PortServer = 5025;
        public Task ConnectListenTcp { get; set; }
        public Task UdpClientListenMessage { get; set; }
        public ClientStatus ClientStatus { get; set; } = new();
        private NetWorkSend addressServer;
        public static NetWorkMB StaticNetWorkMB;
        private NetWorkMB()
        {
            StaticNetWorkMB = this;
        }
        private void Start()
        {
            CommendRouting.IncludeCommands();
        }
        /// <summary>
        /// Главный поток
        /// </summary>
        private void Update()
        {
            BaseCommand.MainUpdate();
        }
        public async void ConnectToServer(string IPAddres, int Port)
        {
            bool SuccessfulConnect = await Connect(IPAddres, Port);
            if (SuccessfulConnect)
            {
                UIDebug.Log($"Succesful connect to {IPAddres}:{Port}");
                await SendRequst(new(nameof(GetOnline)), true);
                await SendRequst(new(nameof(GetWorldObject)), true);
                try
                {
                    GameWorld.StaticGameWorld.FindUnitById(GameStatus.StaticGameStatus.PlayerId).SetCamera();
                    UdpClientListenMessage = ListenUdp(Port);
                    GameStatus.StaticGameStatus.StartGameClient();
                    InvokeRepeating(nameof(SendPingInvoke), 0.5f, 0.5f);
                }
                catch
                {
                    Debug.LogError($"ID player: {GameStatus.StaticGameStatus.PlayerId}");
                    Debug.Break();
                }
            }
            else
            {
                UIDebug.Log($"Unsuccesful connect to {IPAddres}:{Port}");
            }
        }
        public async void Disconnect()
        {
            await SendRequst(new(nameof(Disconected)));
            CancelInvoke(nameof(SendPingInvoke));

            GameWorld.StaticGameWorld.Clear();
            NetWorkPlayers.StaticNetWorkPlayers.Clear();
            GameStatus.StaticGameStatus.EndGameClient();
            NetWorkGet.UdpClosePort();
            addressServer = null;
        }
        public async void ServerClose()
        {
            CancelInvoke(nameof(SyncPositionInvoke));
            GameStatus.StaticGameStatus.EndGameSever();
            GameWorld.StaticGameWorld.Clear();
            addressServer = new();
            addressServer.SetEndPoint("127.0.0.1", PortServer);
            await addressServer.TcpRequst(
                CloseCommand
            );
            addressServer = null;

        }
        public void Exit()
        {
            if(GameStatus.StaticGameStatus.IsOnlineClietn()){
                Disconnect();
            }
            if(GameStatus.StaticGameStatus.IsServer)
            {
                ServerClose();
            }
        }
        public void StartServer()
        {
            IsClient = false;
            Listen(PortServer);
            GameStatus.StaticGameStatus.StartGameServer();
            InvokeRepeating(nameof(SyncPositionInvoke), 0, 0.04f);
        }
        private void Listen(int port)
        {
            ConnectListenTcp = ListenTcp(port);
        }
        private async Task ListenUdp(int port)
        {
            try
            {
                NetWorkGet.UdpOpenPort(port);

                while (true)
                {
                    string command = await NetWorkGet.UdpGetMessage();
                    if (!GameStatus.StaticGameStatus.IsOnlineClietn())
                    {
                        break;
                    }
                    CommendRouting.CommandRout(command, "udp", "");
                }
            }
            catch (System.Exception e)
            {
                UIDebug.Log(e.Message);
            }
        }
        private async Task ListenTcp(int port)
        {
            try
            {

                NetWorkGet.TcpInitServer(port);

                while (true)
                {

                    string resultWhile = "";
                    await NetWorkGet.TcpListenMessage(
                        (string responce, NetWorkGet.StringResult result, string ipAddress) =>
                        {
                            resultWhile = result.str;
                            result.str = CommendRouting.CommandRout(responce, "tcp", ipAddress);
                        }
                    );
                    if(resultWhile == CloseCommand)
                    {
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                Debug.LogError(e);
            }
            NetWorkGet.TcpCloseServer();
        }
        private async Task<bool> Connect(string IPAddres, int Port)
        {
            addressServer = new();
            addressServer.SetEndPoint(IPAddres, Port);

            await SendRequst(new(nameof(TryConnect)), true);
            return true;
        }
        public void SetNameToCommend(ref CommandTemplate command)
        {
            command.UserName = GameStatus.StaticGameStatus.PlayerName;
            command.Id = GameStatus.StaticGameStatus.PlayerId;
        }
        public async Task SendRequst(CommandTemplate Command, bool responce = false)
        {
            try
            {

            SetNameToCommend(ref Command);

            if (responce)
            {
                await addressServer.TcpRequst(
                    Command.ToString(),
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
                  Command.ToString()
              );
            } 
            }
            catch
            {
            }
        }
        private void SyncPositionInvoke()
        {
            foreach (KeyValuePair<string, NetWorkPlayer> player in NetWorkPlayers.StaticNetWorkPlayers.PlayersList)
            {
                if(player.Value.NetWorkSender == null)
                {
                    continue;
                }
                World world = GameWorld.StaticGameWorld.GetWorld();
                foreach (NetWork.TypeJsonBody.GameObject element in world.objects)
                {
                    try
                    {
                        CommandTemplate command = new(nameof(MoveWorldObject));
                        command.SetJsonBody(element);
                        player.Value.NetWorkSender.UdpSend(command.ToString());
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
        }
        private async void SendPingInvoke()
        {
            await SendRequst(new(nameof(PingClient)));
        }
        private void ClearOflinePlayer()
        {
            foreach (KeyValuePair<string, NetWorkPlayer> player in NetWorkPlayers.StaticNetWorkPlayers.PlayersList)
            {
                player.Value.CheckOneline();
            }
        }
    }
}

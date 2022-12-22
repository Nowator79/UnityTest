using NetWork.TypeJsonBody;
using Scripts;
using System;
using UnityEngine;

public class TryConnect : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        try
        {
            string name = command.UserName;
            UIDebug.Log($"1");
            Unit player = DataBase.DataBase.StaticDateBase.UnitsDataBase.Units[(int)DataBase.Units.UnitsList.Player].CreateObject();
            UIDebug.Log($"Player connected {name}");

            Scripts.Modules.NetWork.NetWorkSend client = new();
            client.SetEndPoint(ipAddress, NetWorkMB.PortServer);
            client.UdpConnect();

            NetWorkPlayers.StaticNetWorkPlayers.Add(name, client);


            CommandTemplate commandTemplate = new()
            {
                TypeCommandStr = "SuccessfulConnect",
            };
            commandTemplate.SetJsonBody(new ServerInfo(player.ID, "serverName"));

            string result = commandTemplate.ToString();
            UIDebug.Log($"ROUT {result}");

        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}

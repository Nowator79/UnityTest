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
            Unit player = DataBase.DataBase.StaticDateBase.UnitsDataBase.Units[(int)DataBase.Units.UnitsList.Player].CreateObject();

            Scripts.Modules.NetWork.NetWorkSend client = new();
            client.SetEndPoint(ipAddress, NetWorkMB.PortServer);
            client.UdpConnect();

            NetWorkPlayers.StaticNetWorkPlayers.Add(name, client);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    public override string PreProcess(CommandTemplate command, string ipAddress)
    {
        base.PreProcess(command, ipAddress);

        CommandTemplate commandTemplate = new(nameof(SuccessfulConnect));

        commandTemplate.SetJsonBody(new ServerInfo(1, "serverName"));

        string result = commandTemplate.ToString();
        return result;
    }
}

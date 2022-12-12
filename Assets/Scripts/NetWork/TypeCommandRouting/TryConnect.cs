using NetWork.TypeJsonBody;

public class TryConnect : BaseCommand
{
    public override string Start(CommandTemplate command)
    {

        
        string name = command.UserName;
        Unit player = DataBase.DataBase.StaticDateBase.UnitsDataBase.Units[(int)DataBase.Units.UnitsList.Player].CreateObject();
        NetWorkPlayers.StaticNetWorkPlayers.Add(name);
        UIDebug.Log($"Player connected {name}");

        CommandTemplate commandTemplate = new()
        {
            TypeCommandStr = "SuccessfulConnect",
        };
        commandTemplate.SetJsonBody(new ServerInfo(player.ID, "serverName"));

        string result = commandTemplate.ToString();
        return result;
    }
}

using Scripts;

public class Disconected : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        string result = "";
        NetWorkPlayers.StaticNetWorkPlayers.RemoveByName(command.UserName);
        UIDebug.Log(NetWorkPlayers.StaticNetWorkPlayers.GetType().Name);
        UIDebug.Log($"Disconect: {command.UserName}");
        return result;
    }
}

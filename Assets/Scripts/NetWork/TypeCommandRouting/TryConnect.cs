public class TryConnect : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        string result = new CommandTemplate()
        {
            TypeCommandStr = "SuccessfulConnect",
        }.ToString();
        string name = command.UserName;
        Unit player = DataBase.DataBase.StaticDateBase.UnitsDataBase.Units[(int)DataBase.Units.UnitsList.Player].CreateObject();
        NetWorkPlayers.StaticNetWorkPlayers.Add(name);
        UIDebug.Log($"Player connected {name}");
        return result;
    }
}

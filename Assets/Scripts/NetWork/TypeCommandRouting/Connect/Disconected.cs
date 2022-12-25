public class Disconected : BaseCommand
{
    public override void  Process(CommandTemplate command, string ipAddress)
    {
        NetWorkPlayers.StaticNetWorkPlayers.RemoveByName(command.UserName);
        GameWorld.StaticGameWorld.RemoveById(command.Id);
        UIDebug.Log($"Disconect: {command.UserName}");
    }
}

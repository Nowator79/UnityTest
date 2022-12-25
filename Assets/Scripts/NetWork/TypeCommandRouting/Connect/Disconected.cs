public class Disconected : BaseCommand
{
    public override void  Process(CommandTemplate command, string ipAddress)
    {
        NetWorkPlayers.StaticNetWorkPlayers.DisonectClient(command.UserName);
    }

}

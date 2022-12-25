public class PingServer : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        NetWorkPlayers.StaticNetWorkPlayers.FindByName(command.UserName).Ping();
    }
}

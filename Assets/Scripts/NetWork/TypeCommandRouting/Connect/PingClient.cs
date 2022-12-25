public class PingClient : BaseCommand
{
    public override string PreProcess(CommandTemplate command, string ipAddress)
    {
        CommandTemplate pingCommand = new(nameof(PingServer));
        return pingCommand.ToString();
    }
}

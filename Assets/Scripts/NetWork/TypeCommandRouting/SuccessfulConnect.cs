using NetWork.TypeJsonBody;
using Scripts;

public class SuccessfulConnect : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        NetWorkMB.StaticNetWorkMB.ClientStatus.ConnectSuccesful();
        command.GetJsonBody<ServerInfo>();
        return "";
    }
}

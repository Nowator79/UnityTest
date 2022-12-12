using NetWork.TypeJsonBody;
using Newtonsoft.Json;
using Scripts;

public class SuccessfulConnect : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        NetWorkMB.StaticNetWorkMB.ClientStatus.ConnectSuccesful();
        ServerInfo serverInfo = command.GetJsonBody<ServerInfo>();
        GameStatus.StaticGameStatus.PlayerId = serverInfo.PlayerId;

        return "";
    }
}

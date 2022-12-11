using Scripts;

public class SuccessfulConnect : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        string result = "";
        NetWorkMB.StaticNetWorkMB.ClientStatus.ConnectSuccesful();
        return result;
    }
}

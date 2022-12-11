using System;

public class ConnectCommand : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        UIDebug.Log("Connect");
        return "Connect";
    }
}

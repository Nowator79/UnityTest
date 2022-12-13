using System;

public class ConnectCommand : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        UIDebug.Log("Connect");
        return "Connect";
    }
}

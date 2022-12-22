using System;

public class DisconnectCommand : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        UIDebug.Log("Disonnect");
    }
}

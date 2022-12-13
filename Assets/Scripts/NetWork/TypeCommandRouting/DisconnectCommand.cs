using System;

public class DisconnectCommand : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        UIDebug.Log("Disonnect");
        return "Disonnect";
    }
}

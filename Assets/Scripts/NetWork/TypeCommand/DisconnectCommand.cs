using System;

public class DisconnectCommand : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        UIDebug.Log("Disonnect");
        return "Disonnect";
    }
}

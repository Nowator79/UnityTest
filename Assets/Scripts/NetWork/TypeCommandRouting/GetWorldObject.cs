using NetWork.TypeJsonBody;
using System.Collections.Generic;

public class GetWorldObject : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        CommandTemplate resultCommand = new() { TypeCommandStr = "SetWorldObject" };
        World world = GameWorld.StaticGameWorld.GetWorld();
        resultCommand.SetJsonBody(world);
        return resultCommand.ToString();
    }
}

using NetWork.TypeJsonBody;

public class GetWorldObject : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        CommandTemplate resultCommand = new("SetWorldObject");
        World world = GameWorld.StaticGameWorld.GetWorld();
        resultCommand.SetJsonBody(world);
        return resultCommand.ToString();
    }
}

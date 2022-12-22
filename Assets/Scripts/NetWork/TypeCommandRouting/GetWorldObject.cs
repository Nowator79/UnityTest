using NetWork.TypeJsonBody;

public class GetWorldObject : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        CommandTemplate resultCommand = new("SetWorldObject");
        World world = GameWorld.StaticGameWorld.GetWorld();
        resultCommand.SetJsonBody(world);
    }
}

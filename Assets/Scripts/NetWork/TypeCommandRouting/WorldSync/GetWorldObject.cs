using NetWork.TypeJsonBody;

public class GetWorldObject : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
   
    }
    public override string PreProcess(CommandTemplate command, string ipAddress)
    {
        base.PreProcess(command, ipAddress);
        CommandTemplate resultCommand = new(nameof(SetWorldObject));
        World world = GameWorld.StaticGameWorld.GetWorld();
        resultCommand.SetJsonBody(world);
        return resultCommand.ToString();
    }
}

using NetWork.TypeJsonBody;
using System.Collections.Generic;

public class GetWorldObject : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        CommandTemplate resultCommand = new() { TypeCommandStr = "SetWorldObject" };
        List<World.GameObject> objects = new();

        foreach (Unit unity in GameWorld.StaticGameWorld.UnitsList)
        {
            objects.Add(new World.GameObject(unity.ID, unity.IdType, new(unity.transform.position), new(unity.transform.rotation.eulerAngles)));
            
        }
        World world = new(objects);

        resultCommand.SetJsonBody(world);
        return resultCommand.ToString();
    }
}

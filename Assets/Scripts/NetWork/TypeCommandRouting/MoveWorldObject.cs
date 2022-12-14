using NetWork.TypeJsonBody;

public class MoveWorldObject : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        World world = command.GetJsonBody<World>();

        foreach (var element in world.objects)
        {
            GameWorld.StaticGameWorld.FindUnitById(element.Id).transform. SetPositionAndRotation(element.Position.GetVector3(), element.Rotation.GetQuaternion());
        }

        return "";
    }

}

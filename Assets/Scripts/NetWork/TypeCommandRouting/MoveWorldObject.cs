using NetWork.TypeJsonBody;

public class MoveWorldObject : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        GameObject element = command.GetJsonBody<GameObject>();
        GameWorld.StaticGameWorld.FindUnitById(element.Id).transform. SetPositionAndRotation(element.Position.GetVector3(), element.Rotation.GetQuaternion());
        return "";
    }

}

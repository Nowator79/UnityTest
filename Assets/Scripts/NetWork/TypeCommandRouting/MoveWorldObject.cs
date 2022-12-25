using NetWork.TypeJsonBody;

public class MoveWorldObject : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        GameObject element = command.GetJsonBody<GameObject>();
        Unit unit = GameWorld.StaticGameWorld.FindUnitById(element.Id);
        if (element.UpdateTime > unit.LastUpdate)
        {
            unit.LastUpdate = element.UpdateTime;
            unit.SetNetWork(element.Position.GetVector3(), element.Rotation.GetQuaternion());
        }
    }

}

using NetWork.TypeJsonBody;
using System;

public class MoveWorldObject : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        GameObject element = command.GetJsonBody<GameObject>();
        Unit unit = GameWorld.StaticGameWorld.FindUnitById(element.Id);
        if (element.UpdateTime > unit.LastUpdate)
        {
            unit.LastUpdate = element.UpdateTime;
            unit.transform.SetPositionAndRotation(element.Position.GetVector3(), element.Rotation.GetQuaternion());
        }
        return "";
    }

}

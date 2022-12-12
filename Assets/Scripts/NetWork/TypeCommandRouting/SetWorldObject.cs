using NetWork.TypeJsonBody;
using UnityEngine;

public class SetWorldObject : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        World world = command.GetJsonBody<World>();
        foreach (World.GameObject element in world.objects)
        {
            Unit unit = DataBase.DataBase.StaticDateBase.UnitsDataBase.Units[element.IdType].CreateObject();
            unit.ID = element.Id;
            unit.transform.SetPositionAndRotation(new(element.Position.X, element.Position.Y, element.Position.Z), Quaternion.Euler(new(element.Rotation.X, element.Rotation.Y, element.Rotation.Z)));
        }
        return "";
    }
}

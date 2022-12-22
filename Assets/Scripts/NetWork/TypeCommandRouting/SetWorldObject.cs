using NetWork.TypeJsonBody;

public class SetWorldObject : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        World world = command.GetJsonBody<World>();
        foreach (var element in world.objects)
        {
            Unit unit = DataBase.DataBase.StaticDateBase.UnitsDataBase.Units[element.IdType].CreateObject();
            unit.ID = element.Id;
            unit.transform.SetPositionAndRotation(element.Position.GetVector3(), element.Rotation.GetQuaternion());
        }
    }
}

using NetWork.TypeJsonBody;

public class SetWorldObject : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        World world = command.GetJsonBody<World>();
        foreach (World.GameObject element in world.objects)
        {
            DataBase.DataBase.StaticDateBase.UnitsDataBase.Units[element.Id].CreateObject();
        }
        return "";
    }
}

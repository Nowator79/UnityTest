using Scripts;
using UnityEditor.PackageManager;

public class Disconected : BaseCommand
{
    public override void  Process(CommandTemplate command, string ipAddress)
    {
        string result = "";
        NetWorkPlayers.StaticNetWorkPlayers.RemoveByName(command.UserName);
        GameWorld.StaticGameWorld.RemoveById(command.Id);
        UIDebug.Log($"Disconect: {command.UserName}");
    }
}

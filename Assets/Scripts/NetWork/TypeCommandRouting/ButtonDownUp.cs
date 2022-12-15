using static NetWork.TypeJsonBody.PlayersListOnline;

public class ButtonDownUp : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        ButtonNetWork buttonNetWork = command.GetJsonBody<ButtonNetWork>();
        UIDebug.Log($"Player {command.Id}: {buttonNetWork.Name} is {buttonNetWork.IsClick}");

        Player player = GameWorld.StaticGameWorld.FindUnitById(command.Id).GetComponent<Player>();

        player.ActiveButton(buttonNetWork.Name, buttonNetWork.IsClick);
        return "";
    }
}

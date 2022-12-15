using static NetWork.TypeJsonBody.PlayersListOnline;

public class ButtonDownUp : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        ButtonNetWork buttonNetWork = command.GetJsonBody<ButtonNetWork>();
        Player player = GameWorld.StaticGameWorld.FindUnitById(command.Id).GetComponent<Player>();

        player.ActiveButton(buttonNetWork.Name, buttonNetWork.IsClick);
        return "";
    }
}

using NetWork.TypeJsonBody;

public class OnlineList : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        PlayersListOnline playersList = command.GetJsonBody<PlayersListOnline>();
        foreach (PlayersListOnline.Player player in playersList.PlayerList)
        {
            NetWorkPlayers.StaticNetWorkPlayers.Add(player.Name);
        }
        return "";
    }
}

using NetWork.TypeJsonBody;

public class OnlineList : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        PlayersListOnline playersList = command.GetJsonBody<PlayersListOnline>();
        foreach (PlayersListOnline.Player player in playersList.PlayerList)
        {
            NetWorkPlayers.StaticNetWorkPlayers.PlayersList.Add(player.Name, new NetWorkPlayer(player.Name));
        }
        return "";
    }
}

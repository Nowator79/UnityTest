using NetWork.TypeJsonBody;

public class OnlineList : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        PlayersListOnline playersList = command.GetJsonBody<PlayersListOnline>();
        foreach (PlayersListOnline.Player player in playersList.PlayerList)
        {
            NetWorkPlayers.StaticNetWorkPlayers.Add(player.Name);
        }
    }
}

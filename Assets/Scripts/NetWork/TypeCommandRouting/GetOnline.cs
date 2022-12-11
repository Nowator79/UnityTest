public class GetOnline : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        CommandTemplate resultCommand = new() { TypeCommandStr = "OnlineList" };
        PlayersListOnline playersList = new();

        foreach (var player in NetWorkPlayers.StaticNetWorkPlayers.PlayersList)
        {
            playersList.PlayerList.Add(new PlayersListOnline.Player() { Name= player.Value.Name });
        }

        resultCommand.SetJsonBody(playersList);
        return resultCommand.ToString();
    }
}

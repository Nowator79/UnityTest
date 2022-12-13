using NetWork.TypeJsonBody;

public class GetOnline : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        CommandTemplate resultCommand = new() { TypeCommandStr = "OnlineList" };
        NetWork.TypeJsonBody.PlayersListOnline playersList = new();
        playersList.PlayerList = new System.Collections.Generic.List<PlayersListOnline.Player>();
        foreach (var player in NetWorkPlayers.StaticNetWorkPlayers.PlayersList)
        {
            playersList.PlayerList.Add(new PlayersListOnline.Player() { Name= player.Value.Name });
        }

        resultCommand.SetJsonBody(playersList);
        return resultCommand.ToString();
    }
}

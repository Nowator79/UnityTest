using NetWork.TypeJsonBody;
using UnityEngine;

public class GetOnline : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
    
    }
    public override string PreProcess(CommandTemplate command, string ipAddress)
    {
        base.PreProcess(command, ipAddress);
        CommandTemplate resultCommand = new(nameof(OnlineList));
        NetWork.TypeJsonBody.PlayersListOnline playersList = new()
        {
            PlayerList = new System.Collections.Generic.List<PlayersListOnline.Player>()
        };

        foreach (var player in NetWorkPlayers.StaticNetWorkPlayers.PlayersList)
        {
            playersList.PlayerList.Add(new PlayersListOnline.Player() { Name = player.Value.Name });
        }

        resultCommand.SetJsonBody(playersList);
        return resultCommand.ToString();
    }
}

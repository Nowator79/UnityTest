using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public static GameStatus StaticGameStatus;
    private GameStatus() { StaticGameStatus = this; }

    private GameWorld GameWorld;
    public string PlayerName = "";
    public int PlayerId;
    public bool IsServer { get; private set; } = false;
    public bool IsGameing { get; private set; } = false;

    private void Start()
    {
        GameWorld = GetComponent<GameWorld>();
        UIMainMenu.StaticUIMainMenu.Show();
    }
    public bool IsOnlineClietn()
    {
        return !IsServer && IsGameing;
    }

    public void StartGameServer() {
        GameWorld.InintGameWorld();
        IsGameing = true;
        IsServer = true;
    }
    public void EndGameSever()
    {
        IsGameing = false;
        IsServer = false;

    }
    public void StartGameClient()
    {
        IsGameing = true;
        IsServer = false;
    }
    public void EndGameClient()
    {
        IsGameing = false;
        IsServer = false;
    }
}

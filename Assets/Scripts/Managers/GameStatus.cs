using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private GameWorld GameWorld;
    public string PlayerName = "";
    public int PlayerId;
    public bool IsServer { get; private set; } = false;
    private void Start()
    {
        GameWorld = GetComponent<GameWorld>();
        UIMainMenu.StaticUIMainMenu.Show();
    }

    private bool isGameing;
    public bool IsGameing { get { return isGameing; } }
    public void StartGameServer() {
        GameWorld.InintGameWorld();
        isGameing = true;
        IsServer = true;
    }
    public void EndGameSever()
    {
        isGameing = false;
        IsServer = false;

    }

    public static GameStatus StaticGameStatus;
    private GameStatus() { StaticGameStatus = this; }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private GameWorld GameWorld;
    public string PlayerName = "";
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
    }

    public static GameStatus StaticGameStatus;
    private GameStatus() { StaticGameStatus = this; }

}

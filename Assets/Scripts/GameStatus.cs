using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private GameWorld GameWorld;

    private void Start()
    {
        GameWorld = GetComponent<GameWorld>();
    }

    private bool isGameing;
    public bool IsGameing { get { return isGameing; } }
    public void StartGame() {
        GameWorld.InintGameWorld();
        isGameing = true; 
    }

    public static GameStatus StaticGameStatus;
    private GameStatus() { StaticGameStatus = this; }
}

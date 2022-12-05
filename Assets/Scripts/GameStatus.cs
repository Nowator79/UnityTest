using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private bool isGameing;
    public bool IsGameing { get { return isGameing; } }
    public void StartGame() { isGameing = true; }

    private static GameStatus gameStatus;
    private GameStatus() { gameStatus = this; }
    public static GameStatus Get() { return gameStatus; }
}

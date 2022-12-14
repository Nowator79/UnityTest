using Scripts;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private readonly string[] buttonNames = { "w", "a", "s", "d", "space"};
    private void Update()
    {
        if (GameStatus.StaticGameStatus.IsGameing && !GameStatus.StaticGameStatus.IsServer)
        {
            Down(buttonNames);
        }
    }
    private void Down(string[] btns)
    {
        foreach (string btn in btns)
        {
            if (Input.GetKeyDown(btn))
            {
                Send(btn, true);
                Debug.Log($"Down {btn}");
            }
            else if(Input.GetKeyUp(btn))
            {
                Send(btn, false);
                Debug.Log($"Up {btn}");
            }
        }

        static void Send(string btn, bool isDown)
        {
            ButtonNetWork btnNetWork = new(btn, isDown);
            CommandTemplate command = new("ButtonDownUp");
            command.SetJsonBody(btnNetWork);
            NetWorkMB.StaticNetWorkMB.SendRequst(command);
        }
    }
}

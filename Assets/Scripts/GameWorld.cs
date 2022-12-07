using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    private GameStatus GameStatus;
    public void InintGameWorld()
    {
        Unit player = DataBase.StaticDateBase.UnitsDataBase.Units[0].InitObject(new Vector3(0, 0, 0));
        CameraMove.StaticCameraMove.SetTarget(player.transform);
    }
}

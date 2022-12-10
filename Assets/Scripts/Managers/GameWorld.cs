using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public static GameWorld StaticGameWorld; 
    private GameWorld()
    {
        StaticGameWorld = this;
    }
    public List<Unit> UnitsList = new List<Unit>();
    public void InintGameWorld()
    {
        Unit player = DataBase.StaticDateBase.UnitsDataBase.Units[0].InitObject(new Vector3(0, 0, 0));
        CameraMove.StaticCameraMove.SetTarget(player.transform);

        UnitsList.Add(player);
    }
    public void Destroy()
    {
        foreach (Unit unit in UnitsList)
        {
            Destroy(unit.gameObject);
        }
        UnitsList.Clear();
        CameraMove.StaticCameraMove.SetTarget(gameObject.transform);
    }
}

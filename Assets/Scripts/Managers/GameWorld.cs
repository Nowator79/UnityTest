using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public static GameWorld StaticGameWorld; 
    private GameWorld()
    {
        StaticGameWorld = this;
    }
    public List<Unit> UnitsList = new();
    public void InintGameWorld()
    {
        Player player = (Player)DataBase.DataBase.StaticDateBase.UnitsDataBase.Units[(int)DataBase.Units.UnitsList.Player].CreateObject();
        player.SetControl();
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
    public Unit FindUnitById(int Id)
    {
        return UnitsList.Where(p => p.ID == Id).FirstOrDefault();
    }
}

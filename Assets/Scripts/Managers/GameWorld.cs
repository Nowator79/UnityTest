using NetWork.TypeJsonBody;
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
    
    public World GetWorld() 
    {
        List<NetWork.TypeJsonBody.GameObject> objects = new();

        foreach (Unit unity in UnitsList)
        {
            objects.Add(new NetWork.TypeJsonBody.GameObject(unity.ID, unity.IdType, new(unity.transform.position), new(unity.transform.rotation.eulerAngles)));
        }
        World world = new(objects);
        return world;
    }
}

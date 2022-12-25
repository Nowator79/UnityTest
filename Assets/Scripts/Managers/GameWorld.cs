using NetWork.TypeJsonBody;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public static GameWorld StaticGameWorld { get; set; }
    private World CashWorld { get; set; }
    public List<Unit> UnitsList { get; set; } = new();
    private GameWorld()
    {
        StaticGameWorld = this;
    }
    private void Update()
    {
        List<NetWork.TypeJsonBody.GameObject> objects = new();

        foreach (Unit unity in UnitsList)
        {
            objects.Add(new NetWork.TypeJsonBody.GameObject(unity.ID, unity.IdType, new(unity.transform.position), new(unity.transform.rotation.eulerAngles)));
        }
        World world = new(objects);
        CashWorld = world;
    }
    public void InintGameWorld()
    {
        Player player = (Player)DataBase.DataBase.StaticDateBase.UnitsDataBase.Units[(int)DataBase.Units.UnitsList.Player].CreateObject();
        player.SetControl();
        NetWorkPlayers.StaticNetWorkPlayers.Add(GameStatus.StaticGameStatus.PlayerName);
    }
    public void Clear()
    {
        foreach (Unit unit in UnitsList)
        {
            Destroy(unit.gameObject);
        }
        UnitsList.Clear();
        CameraMove.StaticCameraMove.SetTarget(gameObject.transform);
        NetWorkPlayers.StaticNetWorkPlayers.Clear();
    }
    public Unit FindUnitById(int id)
    {
        return UnitsList.Where(p => p.ID == id).FirstOrDefault();
    }
    public World GetWorld() 
    {
        return CashWorld;
    }
    public void RemoveById(int id)
    {
        Unit removedUnit = FindUnitById(id);
        UnitsList.Remove(removedUnit);
        Destroy(removedUnit.gameObject);
    }
}

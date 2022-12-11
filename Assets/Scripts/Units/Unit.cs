using UnityEngine;

public class Unit : MonoBehaviour
{
    public int IdType = 0;
    public int ID = 0;
    public void Print()
    {
        Debug.Log($"{gameObject.name}");
    }
    public Unit CreateObject(Vector3 position = new Vector3())
    {
        Unit unit = Instantiate(gameObject).GetComponent<Unit>();
        unit.transform.position = position;
        GameWorld.StaticGameWorld.UnitsList.Add(unit);

        return unit;
    }
    public void SetCamera()
    {
        CameraMove.StaticCameraMove.SetTarget(transform);
    }
}

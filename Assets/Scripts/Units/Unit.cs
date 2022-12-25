using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int IdType = 0;
    public int ID = 0;
    public static int IDIter = 0;
    public long LastUpdate;
    private Vector3 NetWorkPosition;
    private Quaternion NetWorkRotation;
    private float timeLerp = 0.3f;
    protected virtual void Update()
    {
        transform.position = Vector3.Lerp(transform.position, NetWorkPosition, timeLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, NetWorkRotation, timeLerp);
    }
    public void Print()
    {
        Debug.Log($"{gameObject.name}");
    }
    public Unit CreateObject(Vector3 position = new Vector3())
    {
        Unit unit = Instantiate(gameObject).GetComponent<Unit>();
        unit.transform.position = position;
        GameWorld.StaticGameWorld.UnitsList.Add(unit);
        unit.ID = IDIter++;

        return unit;
    }
    public static int NextPlayerId()
    {
        return IDIter;
    }
    public void SetCamera()
    {
        CameraMove.StaticCameraMove.SetTarget(transform);
    }
    public void SetNetWork(Vector3 position, Quaternion rotation)
    {
        NetWorkPosition = position;
        NetWorkRotation = rotation;
    }
}

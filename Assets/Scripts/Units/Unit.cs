using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int ID = 0;
    public void Print()
    {
        Debug.Log($"{gameObject.name}");
    }
    public Unit InitObject(Vector3 position)
    {
        Unit unit = Instantiate(gameObject).GetComponent<Unit>();
        unit.transform.position = position;
        return unit;
    }
}

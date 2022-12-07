using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public UnitsDataBase UnitsDataBase;
    public static DataBase StaticDateBase;
    private DataBase()
    {
        StaticDateBase = this;
    }
    private void Start()
    {
        for (int i = 0; i < UnitsDataBase.Units.Count; i++)
        {
            Unit unit = UnitsDataBase.Units[i];
            unit.ID = i;
        } 
    }
}

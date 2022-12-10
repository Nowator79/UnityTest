using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UnitsDB", menuName = "DB/UnitsDB")]
public class UnitsDataBase : ScriptableObject
{
    public List<Unit> Units = new ();
}

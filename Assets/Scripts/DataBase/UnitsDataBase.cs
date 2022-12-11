using System.Collections.Generic;
using UnityEngine;

namespace DataBase.Units
{
    [CreateAssetMenu(fileName ="UnitsDB", menuName = "DB/UnitsDB")]
    public class UnitsDataBase : ScriptableObject
    {
        public List<Unit> Units = new ();
    
    }
    public enum UnitsList
    {
        Player = 1,
    }
}
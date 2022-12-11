using DataBase.Units;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DataBase
{
    public class DataBase : MonoBehaviour
    {
        public UnitsDataBase UnitsDataBase;
        public static DataBase StaticDateBase;
        public Dictionary<string, int> TypeUnits = new();
        private DataBase()
        {
            StaticDateBase = this;
        }
    }
}

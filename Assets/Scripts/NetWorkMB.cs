using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using static Scripts.Modules.NetWork;

namespace Scripts
{
    public class NetWorkMB : MonoBehaviour
    {

        [SerializeField]
        private GameObject[] SyncObjArray;
        private void Start()
        {
            StartServer();
        }
        private async void StartServer()
        {
            UdpNetWork.InitServer();
            while (true)
            {
                string command = await UdpNetWork.GetMessage();
                Debug.Log(command);
                CommandRout(command);
            }
        }
        private void CommandRout(string command)
        {
            TypeCommand myObject = JsonConvert.DeserializeObject<TypeCommand>(command);
            Debug.Log(command);
            Debug.Log(myObject.CustomVector3D);
            SyncObjArray[myObject.CustomId].transform.position = myObject.CustomVector3D.ToVector3();
        }
    }
}

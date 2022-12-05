using Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINetWorkForm : UIBase
{
    [SerializeField]
    private TMP_InputField InputIP;
    [SerializeField]
    private TMP_InputField InputPort;
    [SerializeField]
    private NetWorkMB NetWork;

    [SerializeField]
    private string IPDefult;
    [SerializeField]
    private string PortDefult;
    [SerializeField]

    private void Start()
    {
        if (IPDefult != "")
        {
            InputIP.text = IPDefult;
        }
        if (PortDefult != "")
        {
            InputPort.text = PortDefult;
        }
    }

    public void ConnectToServer()
    {
        string ip = InputIP.text;
        int port = Convert.ToInt32(InputPort.text);
        NetWork.ConnectToServer(ip, port);
        Hidden();
    }
    public void CreateServer()
    {
        NetWork.StartServer();
        Hidden();
    }
    public void Cancel()
    {
        CanvasControler.GetCanvas().mainMenu.Show();
        Hidden();
    }

}

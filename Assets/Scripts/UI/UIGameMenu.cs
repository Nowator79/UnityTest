using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Scripts.NetWorkMB;

public class UIGameMenu : UIBase
{
    private bool curStatus;
    private void Start()
    {
        curStatus = gameObject.activeSelf;
    }
    public void Disconnect()
    {
        StaticNetWorkMB.Disconnect();
        CanvasControler.StaticCanvasControler.mainMenu.Show();
    }
    private void Controller()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            curStatus = !curStatus;
            gameObject.SetActive(curStatus);
        }
    }
    public override void Init()
    {
        base.Init();
        CanvasControler.StaticCanvasControler.updateHandler += Controller;
    }
}

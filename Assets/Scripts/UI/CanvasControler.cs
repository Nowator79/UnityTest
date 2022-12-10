using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControler : MonoBehaviour
{
    public static CanvasControler StaticCanvasControler;
    private CanvasControler() { StaticCanvasControler = this; }

    public UIGameMenu gameMenu;
    public UIMainMenu mainMenu;

    public delegate void UpdateHandler();
    public event UpdateHandler updateHandler;

    public List<UIBase> UIListHiddenAouto = new();

    public void HiddenUIs()
    {
        foreach (UIBase ui in UIListHiddenAouto)
        {
            if (!ui.NoHidden)
            {
                ui.Hidden();
            }
        }
    }

    private void Update()
    {
        updateHandler?.Invoke();
    }
    private void Start()
    {
        foreach (UIBase ui in UIListHiddenAouto)
        {
            ui.Init();
        }
    }
}

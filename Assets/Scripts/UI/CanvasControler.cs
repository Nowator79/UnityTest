using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControler : MonoBehaviour
{
    private static CanvasControler canvasControler;
    private CanvasControler() { canvasControler = this; }
    public static CanvasControler GetCanvas() { return canvasControler; }

    public UIGameMenu gameMenu;
    public UIMainMenu mainMenu;


    void Update()
    {
        gameMenu.Controller();
    }
}

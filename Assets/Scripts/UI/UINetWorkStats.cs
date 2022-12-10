using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UINetWorkStats : UIBase
{
    public static UINetWorkStats StaticUINetWorkStats;
    public UINetWorkStats()
    {
        StaticUINetWorkStats = this;
    }
    [SerializeField]
    private List<ElementStatsPlayerOnline> ElementsOnline = new();
    private void Controller()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Show(false);

        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                Hidden();
            }
        }
    }
    public override void Init()
    {
        base.Init();
        CanvasControler.StaticCanvasControler.updateHandler += Controller;
        foreach (ElementStatsPlayerOnline child in GetComponentsInChildren<ElementStatsPlayerOnline>())
        {
            ElementsOnline.Add(child);
            child.Hidden();
        }
    }
    public void Add(NetWorkPlayer player)
    {
        ElementStatsPlayerOnline element = ElementsOnline.Where(p => !p.IsActive).FirstOrDefault();
        element.Set(player.Name);
    }
    public void RemoveByName(NetWorkPlayer player)
    {
        ElementStatsPlayerOnline element = ElementsOnline.Where(p => p.Text.text == player.Name).FirstOrDefault();
        element.Hidden();
    }
    public void RemoveAll()
    {
        foreach (ElementStatsPlayerOnline item in ElementsOnline)
        {
            item.Hidden();
        }
    }

}

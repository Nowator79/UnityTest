using System.Collections.Generic;
using UnityEngine;
using static Scripts.Modules.NetWork;

public class NetWorkPlayers : MonoBehaviour
{
    public static NetWorkPlayers StaticNetWorkPlayers;
    private NetWorkPlayers()
    {
        StaticNetWorkPlayers = this;
    }
    public Dictionary<string, NetWorkPlayer> PlayersList = new();
    public void Add(string Name, NetWorkSend netWorkSend)
    {
        PlayersList.Add(Name, new NetWorkPlayer(Name, netWorkSend));
        UpdateUI();
    }
    public void RemoveByName(string Name)
    {
        PlayersList.Remove(Name);
        UpdateUI();
    }
    public void UpdateUI()
    {
        UINetWorkStats.StaticUINetWorkStats.RemoveAll();
        foreach (KeyValuePair<string, NetWorkPlayer> element in PlayersList)
        {
            UINetWorkStats.StaticUINetWorkStats.Add(element.Value);
        }
    }
    public void Clear()
    {
        UINetWorkStats.StaticUINetWorkStats.RemoveAll();
        PlayersList = new();
    }
}

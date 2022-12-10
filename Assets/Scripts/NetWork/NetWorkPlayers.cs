using System.Collections.Generic;
using UnityEngine;

public class NetWorkPlayers : MonoBehaviour
{
    public static NetWorkPlayers StaticNetWorkPlayers;
    private NetWorkPlayers()
    {
        StaticNetWorkPlayers = this;
    }
    public Dictionary<string, NetWorkPlayer> PlayersList = new();
    public void Add(string Name)
    {
        PlayersList.Add(Name, new NetWorkPlayer(Name));
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
}

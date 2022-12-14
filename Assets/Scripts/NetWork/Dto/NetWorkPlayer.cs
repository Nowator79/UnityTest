using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkPlayer
{
    private static int lastId = 0;
    public int Id { get; private set; }
    public string Name;

    public NetWorkPlayer(string name)
    {
        Id = lastId;
        lastId++;
        Name = name;
    }
}

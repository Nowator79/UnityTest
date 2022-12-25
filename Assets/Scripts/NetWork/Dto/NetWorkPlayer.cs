using System;
using static Scripts.Modules.NetWork;

public class NetWorkPlayer
{
    private static int lastId = 0;
    public int Id { get; private set; }
    public string Name { get; set; }
    public NetWorkSend NetWorkSender { get; set; }
    public long LastPing { get; set; }
    public NetWorkPlayer(string name, NetWorkSend netWorkSend)
    {
        Id = lastId;
        lastId++;
        Name = name;
        NetWorkSender = netWorkSend;
    }
    public NetWorkPlayer(string name)
    {
        Id = lastId;
        lastId++;
        Name = name;
    }
    public void CheckOneline()
    {
        if((LastPing + 20 * 2) < DateTime.Now.Ticks)
        {
            NetWorkPlayers.StaticNetWorkPlayers.DisonectClient(Name);
        }
    }
    public void Ping()
    {
         LastPing = DateTime.Now.Ticks;
    }
}

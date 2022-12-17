using static Scripts.Modules.NetWork;

public class NetWorkPlayer
{
    private static int lastId = 0;
    public int Id { get; private set; }
    public string Name { get; set; }
    public NetWorkSend NetWorkSender { get; set; }
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
}

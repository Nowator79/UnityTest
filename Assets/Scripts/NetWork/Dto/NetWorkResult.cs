public class NetWorkResult
{
    public string Type { get; private set; } = "tcp";
    public string Message { get; }
    public void SetTcp()
    {
        Type = "tcp";
    }
    public void SetUdp()
    {
        Type = "udp";
    }
    public NetWorkResult(string message)
    {
        Message = message;
    }
}
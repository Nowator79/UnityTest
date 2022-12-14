using Newtonsoft.Json;
using System;
using System.Text;

[Serializable]
public struct CommandTemplate
{
    public string TypeCommandStr { get; set; }
    public int Id { get; set; }
    public string UserName { get; set; }
    public string JsonBody { get; set; }

    public CommandTemplate(string typeCommandStr) : this()
    {
        TypeCommandStr = typeCommandStr;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
    public static CommandTemplate PingCommand { get => new() { TypeCommandStr = "ping" }; }
    public void SetJsonBody<Type>(Type jsonBody)
    {
        JsonBody = (JsonConvert.SerializeObject(jsonBody));
    }
    public T GetJsonBody<T>()
    {
        return JsonConvert.DeserializeObject<T>((JsonBody));
    }
    private static string Coding(string json)
    {
        StringBuilder _json = new(json);
        for (int i = 0; i < _json.Length; i++)
        {
            _json[i] = SwapBits(_json[i]);
        }
        return _json.ToString();
    }


    private static char SwapBits(char x)
    {
        return (char)((x & 0x0F) << 4 | (x & 0xF0) >> 4);
    }


}


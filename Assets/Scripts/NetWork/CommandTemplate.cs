using Newtonsoft.Json;
using System;
using System.Text;

[Serializable]
public struct CommandTemplate
{
    public string TypeCommandStr;
    public string CustomStrData;
    public string UserName;
    public string JsonBody;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
    public static CommandTemplate PingCommand { get => new() { TypeCommandStr = "ping" }; }
    public void SetJsonBody<Type>(Type jsonBody)
    {
        JsonBody = Coding(JsonConvert.SerializeObject(jsonBody));
    }
    public T GetJsonBody<T>()
    {
        return JsonConvert.DeserializeObject<T>(Coding(JsonBody));
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


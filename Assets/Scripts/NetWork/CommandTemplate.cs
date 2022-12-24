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
}


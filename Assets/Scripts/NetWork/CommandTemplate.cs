using Newtonsoft.Json;
using System;

[Serializable]
public struct CommandTemplate
{
    public string TypeCommandStr;
    public string CustomStrData;
    public string UserName;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
    public static CommandTemplate PingCommand { get => new() { TypeCommandStr = "ping" }; 
    }
}
  

using Newtonsoft.Json;
using System;

[Serializable]
public struct CommandTemplate
{

    //Id���� ��������
    public static int CurId = 0;
    //ID ������� � ������� 
    public int typeCommandId;
    public string TypeCommandStr;

    public int CustomId;
    public string CustomStrData;
    public Vector3D CustomVector3D;
    public Vector3D CustomRotation3D;


    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
    public static CommandTemplate PingCommand { get => new CommandTemplate() { TypeCommandStr = "ping" }; 
    }
}
  

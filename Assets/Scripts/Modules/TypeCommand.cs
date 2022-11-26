using System;

[Serializable]
public class TypeCommand
{

    //IdТипа комманды
    public static int CurId = 0;
    //ID команда в консоли 
    public int typeCommandId;

    public int CustomId;
    public string CustomStrData;
    public Vector3D CustomVector3D;

    public TypeCommand()
    {
        this.typeCommandId = CurId;

        CurId++;
    }
    public TypeCommand(int typeCommandId)
    {
        this.typeCommandId = typeCommandId;
    }
}
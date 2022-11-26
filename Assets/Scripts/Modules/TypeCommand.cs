using System;

[Serializable]
public class TypeCommand
{

    //Id���� ��������
    public static int CurId = 0;
    //ID ������� � ������� 
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
using UnityEngine;

public class ErrorRequst : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        string result = "";
        Debug.LogError($"ErrorRequst");
    }
}

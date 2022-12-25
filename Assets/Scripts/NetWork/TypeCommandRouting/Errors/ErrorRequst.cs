using UnityEngine;

public class ErrorRequst : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        Debug.LogError($"ErrorRequst");
    }
}

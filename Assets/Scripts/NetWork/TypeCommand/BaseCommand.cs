
using System.Collections.Generic;

public class BaseCommand
{
    public static Dictionary<string, BaseCommand> Commands = new();
    public string Name { get; set; } = "";
    public virtual string Start(CommandTemplate command)
    {
        UIDebug.Log("Обработчик не реализован");
        return "Base";
    }
    public static bool Inclde(BaseCommand command)
    {
        BaseCommand tmp;
        if (Commands.TryGetValue(command.GetType().Name, out tmp))
        {
            UIDebug.Log("Erorr");
        }
        else
        {
            Commands.Add(command.GetType().Name, command);
        }
        return true;
    }
    public static BaseCommand Get(string name)
    {
        BaseCommand tmp;
        if(!Commands.TryGetValue(name, out tmp))
        {
            UIDebug.Log($"Не удалось обработать комманду type: {name}");
        }
        return tmp;
    }
}
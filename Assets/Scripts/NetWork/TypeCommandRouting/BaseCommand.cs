
using System.Collections.Generic;
using UnityEngine;

public class BaseCommand
{
    public static Dictionary<string, BaseCommand> Commands = new();
    private readonly Queue<Instruction> Tasks = new();

    public virtual void Process(CommandTemplate command, string ipAddress)
    {
        Debug.LogError("Обработчик не реализован");
    }
    public static bool Inclde(BaseCommand command)
    {
        string name = command.GetType().Name;

        BaseCommand tmp;
        if (Commands.TryGetValue(name, out tmp))
        {
            UIDebug.Log("Erorr");
        }
        else
        {
            Commands.Add(name, command);
        }
        return true;
    }
    public static BaseCommand FindCommandProcesser(string name)
    {
        BaseCommand tmp;
        if (!Commands.TryGetValue(name, out tmp))
        {
            Debug.LogError($"Не удалось обработать комманду type: {name}");
        }
        return tmp;
    }
    public static void MainUpdate()
    {
        foreach (var command in Commands)
        {
            command.Value.Update();
        }
    }
    public void Update()
    {
        while (Tasks.Count > 0)
        {
            Instruction instruction = Tasks.Dequeue();
            Process(instruction.Command, instruction.IP);
        }
    }
    public virtual string PreProcess(CommandTemplate command, string ipAddress)
    {
        Tasks.Enqueue(new Instruction(command, ipAddress));
        return "";
    }
    public class Instruction
    {
        public CommandTemplate Command { get; }
        public string IP { get; }
        public Instruction(CommandTemplate command, string ip)
        {
            Command = command;
            IP = ip;
        }
    }
}
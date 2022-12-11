using Newtonsoft.Json;
using System;
using UnityEngine;

public static class CommendRouting
{
    public static void IncludeCommands()
    {
        BaseCommand.Inclde(new ConnectCommand());
        BaseCommand.Inclde(new DisconnectCommand());
        BaseCommand.Inclde(new TryConnect());
        BaseCommand.Inclde(new SuccessfulConnect());
        BaseCommand.Inclde(new Successful());
        BaseCommand.Inclde(new Disconected());
        BaseCommand.Inclde(new ErrorRequst());
        BaseCommand.Inclde(new GetOnline());
        BaseCommand.Inclde(new OnlineList());
    }
    public static string CommandRout(string command, string type)
    {
        string result = "";
        try
        {
            CommandTemplate myObject = JsonConvert.DeserializeObject<CommandTemplate>(command);
            switch (type)
            {
                case "tcp": tcpRout(myObject); break;
                case "udp": udpRout(myObject); break;
            }
            void tcpRout(CommandTemplate command)
            {
                if (command.TypeCommandStr != "")
                {
                    result = BaseCommand.Get(myObject.TypeCommandStr).Start(command);
                }
            }
            void udpRout(CommandTemplate command)
            {
                UIDebug.Log($"Не удалось обработать комманду {command.TypeCommandStr}");
            }
        }
        catch (Exception e)
        {
            result = new CommandTemplate()
            {
                TypeCommandStr = "ErrorRequst",
            }.ToString();
            Debug.LogError(e);
            UIDebug.Log($"Не удалось распарсить {command}");
        }
        return result;
    }
}

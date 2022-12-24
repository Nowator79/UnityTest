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
        BaseCommand.Inclde(new GetWorldObject());
        BaseCommand.Inclde(new SetWorldObject());
        BaseCommand.Inclde(new MoveWorldObject());
        BaseCommand.Inclde(new ButtonDownUp());
    }
    public static string CommandRout(string command, string type, string ipAddress)
    {
        string result = "";
        CommandTemplate myObject = new();
        try
        {
            myObject = JsonConvert.DeserializeObject<CommandTemplate>(command);
        }
        catch (Exception e)
        {
            result = new CommandTemplate()
            {
                TypeCommandStr = "ErrorRequst",
            }.ToString();
            Debug.Log(e);
            UIDebug.Log($"Не удалось распарсить {command}");
        }
        try
        {
            switch (type)
            {
                case "tcp": tcpRout(myObject); break;
                case "udp": udpRout(myObject); break;
            }
        }catch(Exception e)
        {
        }
        void tcpRout(CommandTemplate command)
        {
            UIDebug.Log($"Запрос: {myObject.TypeCommandStr}");

            if (myObject.TypeCommandStr != "")
            {
                result = BaseCommand.FindCommandProcesser(myObject.TypeCommandStr).PreProcess(command, ipAddress);
            }
        }
        void udpRout(CommandTemplate command)
        {
            if (command.TypeCommandStr != "")
            {
                result = BaseCommand.FindCommandProcesser(myObject.TypeCommandStr).PreProcess(command, ipAddress);
            }
        }

        return result;
    }
}

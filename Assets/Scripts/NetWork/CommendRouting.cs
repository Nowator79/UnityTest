using Newtonsoft.Json;
using System;
using UnityEngine;

public static class CommendRouting
{
    public static void IncludeCommands()
    {
        BaseCommand.Inclde(new TryConnect());
        BaseCommand.Inclde(new SuccessfulConnect());
        BaseCommand.Inclde(new Disconected());
        BaseCommand.Inclde(new ErrorRequst());
        BaseCommand.Inclde(new GetOnline());
        BaseCommand.Inclde(new OnlineList());
        BaseCommand.Inclde(new GetWorldObject());
        BaseCommand.Inclde(new SetWorldObject());
        BaseCommand.Inclde(new MoveWorldObject());
        BaseCommand.Inclde(new ButtonDownUp());
        BaseCommand.Inclde(new PingClient());
        BaseCommand.Inclde(new PingServer());
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
            result = new CommandTemplate(nameof(ErrorRequst)).ToString();
            Debug.Log(e);
        }
        try
        {
            switch (type)
            {
                case "tcp": tcpRout(myObject); break;
                case "udp": udpRout(myObject); break;
            }
        }catch
        {
        }
        void tcpRout(CommandTemplate command)
        {
            if (myObject.TypeCommandStr != "")
            {
                result = BaseCommand.FindCommandProcesser(myObject.TypeCommandStr).PreProcess(command, ipAddress);
            }
        }
        void udpRout(CommandTemplate command)
        {
            //UIDebug.ClearLog(command.ToString());
            if (command.TypeCommandStr != "")
            {
                result = BaseCommand.FindCommandProcesser(myObject.TypeCommandStr).PreProcess(command, ipAddress);
            }
        }

        return result;
    }
}

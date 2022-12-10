using Newtonsoft.Json;
using Scripts;
using System;
using UnityEngine;

public static class CommendRouting
{
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
                if (myObject.TypeCommandStr != "")
                {
                    //������� ������� ���� ������������
                    switch (myObject.TypeCommandStr)
                    {
                        case "TryConnect":
                            result = new CommandTemplate()
                            {
                                TypeCommandStr = "SuccessfulConnect",
                            }.ToString();
                            string name = command.UserName;
                            NetWorkPlayers.StaticNetWorkPlayers.Add(name);
                            break;
                        case "SuccessfulConnect":
                            NetWorkMB.StaticNetWorkMB.ClientStatus.ConnectSuccesful();
                            UIDebug.Log($"Player connected");
                            break;
                        case "Successful":
                            result = new CommandTemplate()
                            {
                                TypeCommandStr = "Successful",
                            }.ToString();
                            break;
                        case "Disconected":
                            NetWorkPlayers.StaticNetWorkPlayers.RemoveByName(command.UserName);
                            UIDebug.Log($"Disconect: {command.UserName}");
                            break;
                        case "ErrorRequst":
                            UIDebug.Log($"ErrorRequst");
                            break;
                        default:
                            UIDebug.Log($"�� ������� ���������� �������� {command.TypeCommandStr} type: {type}");
                            break;
                    }
                }
            }
            void udpRout(CommandTemplate command)
            {
                UIDebug.Log($"�� ������� ���������� �������� {command.TypeCommandStr}");
            }
        }
        catch (Exception e)
        {
            result = new CommandTemplate()
            {
                TypeCommandStr = "ErrorRequst",
            }.ToString();
            Debug.LogError(e);
            UIDebug.Log($"�� ������� ���������� {command}");
        }
        return result;
    }
}

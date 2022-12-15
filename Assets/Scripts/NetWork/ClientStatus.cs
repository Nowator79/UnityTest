using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientStatus
{
    public bool IsConnect { get; private set; } = false;
    public bool IsDisconnect { get; private set; } = true;
    public bool IsWait { get; private set; } = false;
    public void SetWait()
    {
        IsConnect = false;
        IsDisconnect = false;
        IsWait = true;
    }
    public void ConnectSuccesful()
    {
        IsConnect = true;
        IsDisconnect = false;
        IsWait = false;
    }
    public void ConnectUnSuccessful()
    {
        IsConnect = false;
        IsDisconnect = true;
        IsWait = false;
    }
    public void Disconect()
    {
        IsConnect = false;
        IsDisconnect = true;
        IsWait = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QureyReader : MonoBehaviour
{
    public static QureyReader StaticQureyReader;
    private readonly Queue<NetWorkResult> results = new();
    public void SetProcessing(NetWorkResult result)
    {
        results.Enqueue(result);
    }
    private QureyReader()
    {
        StaticQureyReader = this;
    }
    private void Update()
    {
        while (results.Count > 0)
        {
            NetWorkResult result = results.Dequeue();
            CommendRouting.CommandRout(result.Message, result.Type);
        }
    }
}

using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class UIDebug : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI LogText;
    public static UIDebug _UIDebug;
    private readonly Queue<string> logs = new();

    public static void Log(string log)
    {
        _UIDebug.logs.Enqueue(log);
    }
    private void Start()
    {
        _UIDebug = this;
    }
    private void Update()
    {
        while (logs.Count > 0)
        {
            string result = logs.Dequeue();
            _UIDebug.LogText.text += result + "\n";
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            UIDebug.Log(JsonConvert.SerializeObject(GameWorld.StaticGameWorld.GetWorld()));
            UIDebug.Log(JsonConvert.SerializeObject(GameWorld.StaticGameWorld.GetWorld()).Length.ToString());
        }
    }

}

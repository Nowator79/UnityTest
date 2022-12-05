using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDebug : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI LogText;
    public static UIDebug _UIDebug;
    public static void Log(string log)
    {
        while (true)
        {
            if (_UIDebug)
            {
                _UIDebug.LogText.text += log + "\n";
                break;
            }
        }
    }
    private void Start()
    {
        _UIDebug = this;
    }
}

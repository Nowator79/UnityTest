using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementStatsPlayerOnline : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI Text;
    public bool IsActive = false;
    public void Set(string name)
    {
        gameObject.SetActive(true);
        Text.text = name;
        IsActive = true;
    }

    public void Hidden()
    {
        IsActive = false;
        Text.text = "";
        gameObject.SetActive(false);
    }
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private Vector3 Position;
    void Update()
    {
        if (Target)
        {
            gameObject.transform.position = Target.position + Position;
        }
        else
        {
            Debug.LogError("Добавь цель камере для движения");
        }
    }
}

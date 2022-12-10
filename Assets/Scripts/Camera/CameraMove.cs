using Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMove : MonoBehaviour
{

    public static CameraMove StaticCameraMove;
    private CameraMove()
    {
        StaticCameraMove = this;
    }

    public void SetTarget(Transform transform)
    {
        Target = transform;
    }

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;
[ExecuteInEditMode]
public class LegAnimal : MonoBehaviour
{
    [SerializeField]
    private Transform LegDown;
    private Vector3 globalPointDown;
    [SerializeField]
    private float DistanceStep = 0.7f;
    [SerializeField]
    private Vector3 VectorForward;
    [SerializeField]
    private Vector3 VectorVariation = new Vector3(0, 0, -0.5f);

    private void Start()
    {
        SetDefultDownNewPointLeg();
    }
    private void Update()
    {
        Vector3 hStartPoint = new(transform.position.x, 0, transform.position.z);
        Vector3 hEndPoint = new(LegDown.position.x, 0, LegDown.position.z);
        float dist = Vector3.Distance(hStartPoint + VectorForward, hEndPoint);
        if (dist > DistanceStep)
        {
            SetDownNewPointLeg();
        }
        SetLeg();
    }
    private void SetDownNewPointLeg()
    {
        Ray rightRay = new(transform.position, transform.TransformDirection(Vector3.down));
        if (Physics.Raycast(rightRay, out RaycastHit hit, 2))
        {
            Vector3 position = hit.point;
            globalPointDown = position + transform.TransformDirection(new Vector3(0, 0, 0.7f));
        }
    }
    private void SetDefultDownNewPointLeg()
    {
        SetDownNewPointLeg();
        globalPointDown += VectorVariation;
    }
    private void SetLeg()
    {
        LegDown.transform.position = Vector3.Lerp(LegDown.transform.position, globalPointDown, 0.1f);
    }
}

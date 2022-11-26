using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField]
    private float Height = 10;

    private Vector3 leftF = new Vector3(-1, 0, 1);
    private Vector3 rightF = new Vector3(1, 0, 1);
    private Vector3 leftB = new Vector3(-1, 0, -1);
    private Vector3 rightB = new Vector3(1, 0, -1);
    [SerializeField]
    private float ForseSpeed = 1;
    [SerializeField]
    private float MaxSpeedUp = 1;
    [SerializeField]
    private float LF = 1;
    [SerializeField]
    private float RF = 1;
    [SerializeField]
    private float LB = 1;
    [SerializeField]
    private float RB = 1;

    private float _curLF;
    private float _curRF;
    private float _curLB;
    private float _curRB;

    [SerializeField]
    private float leftForse =  0.05f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_rigidbody.velocity.y < MaxSpeedUp)
        {

            if (transform.position.y < Height)
            {
                MoveUp();
            }
        }
    }
    private void MoveUp()
    {
        _curLF = LF;
        _curRF = RF;
        _curLB = LB;
        _curRB = RB;
        if (transform.rotation.eulerAngles.x > 5)
        {
            _curLF = LF - leftForse;
            _curRF = RF - leftForse;
        }
        else if (transform.rotation.eulerAngles.x < -5)
        {
            _curLF = LB - leftForse;
            _curRF = RB - leftForse;
        }

        _rigidbody.AddForceAtPosition(transform.TransformDirection(new Vector3(0, ForseSpeed * _curLF, 0)), transform.position + transform.TransformDirection(leftF) * Time.deltaTime);
        _rigidbody.AddForceAtPosition(transform.TransformDirection(new Vector3(0, ForseSpeed * _curRF, 0)), transform.position + transform.TransformDirection(rightF) * Time.deltaTime);
        _rigidbody.AddForceAtPosition(transform.TransformDirection(new Vector3(0, ForseSpeed * _curLB, 0)), transform.position + transform.TransformDirection(leftB) * Time.deltaTime);
        _rigidbody.AddForceAtPosition(transform.TransformDirection(new Vector3(0, ForseSpeed * _curRB, 0)), transform.position + transform.TransformDirection(rightB) * Time.deltaTime);
    }
}

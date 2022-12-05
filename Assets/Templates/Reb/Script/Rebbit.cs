using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebbit : MainUnitController
{
    private Rigidbody _rigidbody;
    [SerializeField]
    private Vector3 JumpVector = new Vector3(0, 5, 0);
    private bool _inGround = true;
    [SerializeField]
    private float SpeedRotate = 2;
    [SerializeField]
    private float Speed = 2;
    private bool IsInit = false;
    public override void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (Scripts.NetWorkMB.IsClient)
        {
            base.Start();
            InvokeRepeating(nameof(MoveJump), _timeStart, 2);
        }
        else
        {
            _rigidbody.useGravity = false;
        }

        IsInit = true;
    }
    private void Update()
    {
        if (IsInit)
        {
            if (Scripts.NetWorkMB.IsClient)
            {
                if (_positionTarget.x > 0 || _positionTarget.z > 0)
                {
                    Quaternion rotation = Quaternion.LookRotation(_positionTarget);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, SpeedRotate * Time.deltaTime);
                }

                if (Input.GetKeyDown(KeyCode.Space) && _inGround)
                {
                    MoveJump();
                }
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }

    }
    private void MoveJump()
    {
        Vector3 _jumpVector = JumpVector + transform.TransformDirection(Vector3.forward * Speed);
        _rigidbody.AddForce(_jumpVector, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _inGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        _inGround = false;
    }
}

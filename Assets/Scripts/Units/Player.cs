using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private CharacterController CharacterController;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpForse;
    [SerializeField]
    private float speed;
    private float jumpSpeed = 0;
    [SerializeField]
    private bool IsControl = false;
    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }
    [SerializeField]
    private float Horizontal = 0;
    [SerializeField]
    private float Vertical = 0;
    [SerializeField]
    private bool W, A, S, D, Space;
    protected override void Update()
    {
        base.Update();
        if (GameStatus.StaticGameStatus.IsServer)
        {
            if (IsControl)
            {
                Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetKey(KeyCode.Space));
            }
            else
            {
                Horizontal = 0;
                Vertical = 0;
                if (D) Horizontal++;
                if (A) Horizontal--;
                if (W) Vertical++;
                if (S) Vertical--;
                Move(Horizontal, Vertical, Space);
            }
        }
    }
    private void Move(float horizontal = 0, float vertical = 0, bool jump = false)
    {
        if (CharacterController.isGrounded)
        {
            jumpSpeed = 0;
            if (jump)
            {
                jumpSpeed = jumpForse;
            }
        }
        jumpSpeed += gravity * Time.deltaTime * 3f;
        float dirSpeed = speed * Time.deltaTime;
        Vector3 dir = new(horizontal * dirSpeed, jumpSpeed * Time.deltaTime, vertical * dirSpeed);
        CharacterController.Move(dir);
    }
    public void SetControl()
    {
        IsControl = true;
        CameraMove.StaticCameraMove.SetTarget(transform);
    }
    public void ActiveButton(string btn, bool isClick)
    {
        switch (btn)
        {
            case "w":
                W = isClick;
                break;
            case "a":
                A = isClick;
                break;
            case "s":
                S = isClick;
                break;
            case "d":
                D = isClick;
                break;
            case "space":
                Space = isClick;
                break;
        }
    }
}

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

    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = 0;
        float vertical = 0;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (CharacterController.isGrounded)
        {
            jumpSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpSpeed = jumpForse;
            }
        }
        jumpSpeed += gravity * Time.deltaTime * 3f;
        float dirSpeed = speed * Time.deltaTime;
        Vector3 dir = new (horizontal * dirSpeed, jumpSpeed * Time.deltaTime, vertical * dirSpeed);
        CharacterController.Move(dir);
    }
}

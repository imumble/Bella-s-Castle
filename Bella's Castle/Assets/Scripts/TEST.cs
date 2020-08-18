using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public Rigidbody theRB;
    public float moveSpeed, jumpForce;

    private Vector2 moveInput;

    public LayerMask whatIsGround;
    public Transform groupPoint;
    private bool isGround;

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        theRB.velocity = new Vector3(moveInput.x * moveSpeed, theRB.velocity.y, moveInput.y * moveSpeed);


        RaycastHit hit;
        if(Physics.Raycast(groupPoint.position, Vector3.down, out hit, .3f, whatIsGround))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        if(Input.GetButtonDown("Jump") && isGround)
        {
            theRB.velocity += new Vector3(0f, jumpForce, 0f);
        }
    }
}

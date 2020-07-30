using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerBody;

    private Vector3 inputVector;

    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector3(Input.GetAxis("Horizontal") * 7f, playerBody.velocity.y, Input.GetAxis("Vertical") * 7f);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerBody.AddForce(0, 10f, 0, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        playerBody.velocity = inputVector;
    }
}

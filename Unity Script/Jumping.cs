using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Animator animator;

    [SerializeField]
    private float jumpspeed,gravity;
    private Vector3 movementDirection = Vector3.zero;
    
    //public Rigidbody rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(Input.GetButton("Jump"));
        if (Input.GetButtonDown("Jump"))
        {
            //rb.AddForce(new Vector3(0, 1000, 0), ForceMode.Impulse);
            movementDirection.y = jumpspeed;
        }
        movementDirection.y -= gravity * Time.deltaTime;
        controller.Move(movementDirection * Time.deltaTime);
        animator.SetBool("isRunning", Input.GetAxisRaw("Vertical") != 0);
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            animator.SetBool("isRunning", Input.GetAxisRaw("Horizontal") != 0);
        }
        animator.SetBool("isJumping", !controller.isGrounded);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComp : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private Rigidbody rb = null;
    public Animator anim = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb)
        {
            Debug.Log("No rigid body found");
            return;
        }

        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizontalValue* speed, rb.velocity.y, verticalValue * speed);

        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (transform.localScale.y / 2) + 0.01f);
        //Debug.DrawRay(transform.position,Vector3.down * ((transform.localScale.y / 2) + 0.01f), Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        if (!anim)
        {
            Debug.Log("No Animator");
            return;
        }

        anim.SetFloat("Turn", horizontalValue);

        anim.SetFloat("Forward", verticalValue);
        anim.SetBool("isGrounded", isGrounded);
    }
}

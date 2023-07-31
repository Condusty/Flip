using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private GameObject vCam;

    private int facingDirection;

    private Rigidbody2D rb;
    private Animator anim;
    private CamScript camScript;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        camScript = FindObjectOfType<CamScript>();
    }

    private void Update() 
    {
        Move();
        Jump();

        if(rb.velocity.y != 0)
            anim.SetFloat("yVelocity", rb.velocity.y);
        else
            anim.SetBool("Jump", false);

        if(Input.GetKeyDown(KeyCode.F) && IsGrounded())
        {
            FlipGravity();
        }

        Flip();
    }
    
    public void NormalGravity()
    {
        if(rb.gravityScale < 0)
        {
            rb.gravityScale *= -1;
            jumpForce *= -1;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public void FlipGravity()
    {
        rb.gravityScale *= -1;
        jumpForce *= -1;
        camScript.FlipCam();
    }

    private void Move() 
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed * input, rb.velocity.y);
            
        if(rb.velocity.x != 0)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }

    private void Jump() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("Jump", true);
        }
    }

    private void Flip()
    {

        if(rb.velocity.x < 0 ) 
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            facingDirection = 180;
        } 
        else if(rb.velocity.x > 0 ) 
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingDirection = 0;
        }

        if(transform.rotation.eulerAngles.z == 0 && rb.gravityScale < 0)
        {
            if(facingDirection == 0)
                transform.eulerAngles = new Vector3(0, 180, 180);
            else
                transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if(transform.rotation.eulerAngles.z == 180 && rb.gravityScale > 0)
            transform.eulerAngles = new Vector3(0, facingDirection, 0);
        
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 1 * Mathf.Clamp(rb.gravityScale, -1, 1), whatIsGround);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }
}

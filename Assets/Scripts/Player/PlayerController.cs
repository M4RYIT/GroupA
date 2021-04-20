using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Tunable Variables
    public float speed;
    [SerializeField] float JumpForce;
    [SerializeField] float FallingSpeedMultiplier = 2;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask GroundLM;
    [SerializeField] KeyCode JumpButton;
    public bool canMove = true;   //Cambiabile esternamente per interazione potenziale con altri script


    //Working Variables
    private float moveInput;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, GroundLM);

        if (isGrounded)
        {
            Jump();
            anim.SetBool("IsJumping", false);
        }
        else
        {
            anim.SetBool("IsJumping", true);
        }
        
    }

    private void FixedUpdate()
    {
        Run();
        FastFall();
    }

    void Run()
    {
        if (canMove)
            rb.velocity = new Vector2((moveInput * speed *Time.deltaTime) * 10, rb.velocity.y);

        if (canMove)
        {
            Flip();
        }


        if (moveInput != 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(JumpButton))
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }
    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x = Mathf.Sign(moveInput);
        transform.localScale = scaler;
    }
    void FastFall()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = FallingSpeedMultiplier;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    void Respawn()
    {
        anim.SetTrigger("Hitted");
        //TO ADD RESPAWN MECHANICS OR "GO TO MAIN MENU'" SYSTEM
    }


}

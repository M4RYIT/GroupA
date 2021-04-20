using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Tunable Variables
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float fallingSpeedMultiplier = 2;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask GroundLM;
    [SerializeField] KeyCode jumpButton;
    public bool dead;   //Cambiabile esternamente per interazione potenziale con altri script


    //Working Variables
    private float moveInput;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private Vector3 respawnPoint;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawnPoint = transform.position;
        dead = false;
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, GroundLM);
        
        Jump();

        if (isGrounded || dead)
        {
            anim.SetBool("IsJumping", false);
        }
        else
        {
            anim.SetBool("IsJumping", true);
        }

        //DEBUGGING
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }

    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Run();
            FastFall();
        }
    }

    void Run()
    {
        rb.velocity = new Vector2((moveInput * speed *Time.deltaTime) * 10, rb.velocity.y);

        Flip();
        
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
        if (Input.GetKeyDown(jumpButton) && isGrounded && !dead)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
            rb.gravityScale = fallingSpeedMultiplier;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    public void Die()
    {
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        anim.SetTrigger("Disappear");
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
        dead = true;
        yield return new WaitForSeconds(0.45f);
        rb.gravityScale = 1;
        transform.position = respawnPoint;
        anim.SetTrigger("Appear");
        yield return new WaitForSeconds(0.45f);
        dead = false;
    }

}

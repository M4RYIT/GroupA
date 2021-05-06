using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //Tunable Variables
    [SerializeField] GameObject runDustGO;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float fallingSpeedMultiplier = 2;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask GroundLM;
    [SerializeField] LayerMask BottomLimitLM;
    [SerializeField] KeyCode jumpButton;
    public bool dead;   //Cambiabile esternamente per interazione potenziale con altri script

    //Working Variables
    private float moveInput;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField]bool isGrounded;
    private Vector3 respawnPoint;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawnPoint = transform.position;
        dead = false;
    }

    private void Start()
    {
        GameManager.Instance.OnPlayerDeath += Die;
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, GroundLM);

        if(Physics2D.OverlapCircle(groundCheck.position, checkRadius, BottomLimitLM))
        {
            GameManager.Instance.OnPlayerDeath?.Invoke();
        }


        //IMPUT OUT OF THE FUNCTION FOR THE "BOUNCE" ON MUSHO
        if (Input.GetKeyDown(jumpButton) && isGrounded && !dead)
        {
            Jump();
        }

        anim.SetBool("IsJumping", !(isGrounded | dead));        


        if(!isGrounded || dead)
        {
            runDustGO.SetActive(false);
        }
        else
        {
            runDustGO.SetActive(true);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectable c = collision.GetComponent<Collectable>();

        if (c != null) c.Collect();
    }

    void Run()
    {
        rb.velocity = new Vector2((moveInput * speed *Time.deltaTime) * 10, rb.velocity.y);

        Flip();

        anim.SetBool("IsWalking", moveInput != 0);
    }
    public void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x = Mathf.Sign(moveInput);
        transform.localScale = scaler;
    }
    void FastFall()
    {
        rb.gravityScale = rb.velocity.y < 0 ? fallingSpeedMultiplier : 1;
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

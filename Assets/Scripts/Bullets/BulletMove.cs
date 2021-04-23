using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    public float Speed
    {
        get => speed;
        set { speed = value;  }
    }
    [SerializeField]
    private Vector2 dir;
    public Vector2 Dir
    {
        get => dir;
        set { dir = value; }
    }
    [SerializeField]
    private float lifeTime;

    private float workingLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void OnEnable()
    {
        workingLifeTime = lifeTime;
    }
    private void Update()
    {
        workingLifeTime -= Time.deltaTime;

        if(workingLifeTime <= 0)
        {
            DeactivateBullet();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = dir * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.tag == "Player") //LAYER & = GROUND
        {
            DeactivateBullet();
        }
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
        //CONSIDER TO ADD A PARTICLE FOR THE EXPLOSION
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PoolManager pm;
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
        pm = GameObject.FindGameObjectWithTag("PoolManager").GetComponent<PoolManager>();
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
            BulletEnqueue();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(dir.x * speed * Time.fixedDeltaTime, dir.y * speed * Time.fixedDeltaTime).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.tag == "Player") //LAYER & = GROUND
        {
            BulletEnqueue();
        }
    }

    private void BulletEnqueue()
    {
        pm.ReturnObj(gameObject);
        //CONSIDER TO ADD A PARTICLE FOR THE EXPLOSION
    }




}

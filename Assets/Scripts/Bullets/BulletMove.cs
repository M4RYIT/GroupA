using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private SoundEvent sound;
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
        sound = GetComponent<SoundEvent>();
    }

    private void OnEnable()
    {
        CancelInvoke();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool p = collision.gameObject.tag == "Player";

        if (collision.gameObject.layer == 6 || p) //LAYER & = GROUND
        {
            sound.PlayOneShot("Hit");

            if (p) GameManager.Instance.OnPlayerDeath?.Invoke();

            Invoke(nameof(DeactivateBullet), 0.1f);
        }
    }

    private void DeactivateBullet()
    {       
        gameObject.SetActive(false);
        //CONSIDER TO ADD A PARTICLE FOR THE EXPLOSION
    }
}

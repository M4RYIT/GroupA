using UnityEngine;

public class LockedCameraFollow : MonoBehaviour
{
    GameObject target;
    PlayerController pcScript;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector2 offset;

    public bool forwardOnly;

    [SerializeField]
    float leftLimit, rightLimit, bottomLimit, topLimit;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnPlayerDeath += () => { forwardOnly = false; };
        target = GameObject.FindGameObjectWithTag("Player");
        pcScript = target.GetComponent<PlayerController>();
        Follow();
    }

    private void Update()
    {
        if (!pcScript.dead && !forwardOnly) forwardOnly = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null && forwardOnly)
        {
            Vector3 playerViewportPos = Camera.main.WorldToViewportPoint(target.transform.position + (Vector3)offset);
            if (playerViewportPos.x > 0.5)
            {
                Follow();
            }
        }
        else if(target != null && !forwardOnly)
        {
            Follow();
        }
    }



    void Follow()
    {
        Vector3 targetPos = target.transform.position + (Vector3)offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position,targetPos,Time.deltaTime *smoothFactor);
        smoothPosition.z = -10;
        //transform.position = smoothPosition;

        transform.position = new Vector3
            (
                Mathf.Clamp(smoothPosition.x, leftLimit, rightLimit),
                Mathf.Clamp(smoothPosition.y, bottomLimit, topLimit),
                smoothPosition.z
            ); 
    }

}

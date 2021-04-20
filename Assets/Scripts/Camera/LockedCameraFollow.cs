using UnityEngine;

public class LockedCameraFollow : MonoBehaviour
{
    Transform target;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector2 offset;

    public bool ForwardOnly;

    [SerializeField]
    float leftLimit, rightLimit, bottomLimit, topLimit;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Follow();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null && ForwardOnly)
        {
            Vector3 playerViewportPos = Camera.main.WorldToViewportPoint(target.transform.position + (Vector3)offset);
            Debug.Log(playerViewportPos);
            if (playerViewportPos.x > 0.5)
            {
                Follow();
            }
        }
        else if(target != null && !ForwardOnly)
        {
            Follow();
        }
    }

    
    void Follow()
    {
        Vector3 targetPos = target.position + (Vector3)offset;
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

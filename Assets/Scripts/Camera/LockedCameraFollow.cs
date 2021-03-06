using UnityEngine;

public class LockedCameraFollow : MonoBehaviour
{
    GameObject target;
    PlayerController pcScript;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 offset;

    public bool forwardOnly;

    [SerializeField]
    float leftLimit, rightLimit, bottomLimit, topLimit;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        pcScript = target.GetComponent<PlayerController>();
        GameManager.Instance.OnPlayerDeath += () => { forwardOnly = false; };
        Follow();
    }

    private void Update()
    {
        if (!pcScript.dead && !forwardOnly) forwardOnly = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null) Follow();
    }

    void Follow()
    {
        Vector3 playerViewportPos = Camera.main.WorldToViewportPoint(target.transform.position + (Vector3)offset);

        Vector3 targetPos = (playerViewportPos.x > 0.5 || !forwardOnly)?
                            target.transform.position+offset:
                            new Vector3(transform.position.x, target.transform.position.y + offset.y, offset.z);
        
        Vector3 smoothPosition = Vector3.Lerp(transform.position,targetPos,Time.deltaTime *smoothFactor);
        //transform.position = smoothPosition;

        transform.position = new Vector3
            (
                Mathf.Clamp(smoothPosition.x, leftLimit, rightLimit),
                Mathf.Clamp(smoothPosition.y, bottomLimit, topLimit),
                smoothPosition.z
            ); 
    }

}

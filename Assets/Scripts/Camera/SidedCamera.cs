using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidedCamera : MonoBehaviour
{
    Vector2 screenBounds, halfDim;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        screenBounds = cam.transform.position - cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));        
        halfDim = GetComponent<Collider2D>().bounds.extents;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cam.transform.position.x+screenBounds.x+halfDim.x, Mathf.Infinity),
            transform.position.y,
            transform.position.z);
    }
}

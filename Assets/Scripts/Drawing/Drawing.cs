using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Performs drawing mechanics
public class Drawing : MonoBehaviour
{
    public EventSystem EventSystem;
    public GraphicRaycaster Raycaster;
    [Range(0f,1f)]
    public float DistanceThreshold;

    DrawnObject drawnObj;
    Camera cam;
    Vector2 lastPos;
    List<Vector2> positions;
    DrawManager drawMgr;
    bool invalid = false;
    List<RaycastResult> results;

    void Start()
    {
        cam = Camera.main;
        drawMgr = GetComponent<DrawManager>();
        positions = new List<Vector2>();
    }

    void Update()
    {
        if (drawMgr.CurrentDrawTime > 0)
        {
            if (Input.GetMouseButtonDown(0)) StartDrawing();

            if (invalid) return;

            if (Input.GetMouseButton(0))
            {
                lastPos = cam.ScreenToWorldPoint(Input.mousePosition);

                if (Vector2.Distance(lastPos, SpaceConversion(positions[positions.Count - 1], false)) > DistanceThreshold) SetLineEdge();
            }

            if (Input.GetMouseButtonUp(0)) EndDrawing();

            return;
        }

        if (drawMgr.Active) EndDrawing();
    }

    void EndDrawing()
    {
        drawMgr.Active = false;

        if (drawnObj.Line.positionCount<2)
        {
            Destroy(drawnObj.gameObject);
        }

        drawnObj.enabled = true;
    }

    void StartDrawing()
    {
        results = new List<RaycastResult>();
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        Raycaster.Raycast(new PointerEventData(EventSystem) { position = Input.mousePosition }, results);

        invalid = Physics2D.Raycast(r.origin, r.direction).collider != null | results.Count > 0;                  

        if (invalid) return;

        drawMgr.Active = true;        

        positions.Clear();
        lastPos = cam.ScreenToWorldPoint(Input.mousePosition);
        drawnObj = drawMgr.DrawnObject(lastPos);

        SetLineEdge();
    }

    Vector2 SpaceConversion(Vector2 v, bool local = true)
    {
        return local?drawnObj.transform.InverseTransformPoint(v): drawnObj.transform.TransformPoint(v);
    }

    void SetLineEdge()
    {
        positions.Add(SpaceConversion(lastPos));
        drawnObj.Line.positionCount++;
        drawnObj.Line.SetPosition(drawnObj.Line.positionCount - 1, SpaceConversion(lastPos));
        drawnObj.Edge.points = positions.ToArray();
    }
}

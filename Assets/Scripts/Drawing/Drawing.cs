using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Performs drawing mechanics
public class Drawing : MonoBehaviour
{
    [Range(0f,1f)]
    public float DistanceThreshold;

    LineRenderer line;
    EdgeCollider2D edge;
    DrawnObject drawnObj;
    Camera cam;
    Vector2 lastPos;
    List<Vector2> positions;
    DrawManager drawMgr;

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

            if (Input.GetMouseButton(0))
            {
                lastPos = cam.ScreenToWorldPoint(Input.mousePosition);

                if (Vector2.Distance(lastPos, positions[positions.Count - 1]) >= DistanceThreshold) SetLineEdge();
            }

            if (Input.GetMouseButtonUp(0)) EndDrawing();

            return;
        }

        if (drawMgr.Active) EndDrawing();
    }

    void EndDrawing()
    {
        drawMgr.Active = false;
        drawnObj.enabled = true;
    }

    void StartDrawing()
    {
        drawMgr.Active = true;

        positions.Clear();
        lastPos = cam.ScreenToWorldPoint(Input.mousePosition);

        GameObject obj = Instantiate(drawMgr.DrawnObjectPrefab, lastPos, Quaternion.identity);
        line = obj.GetComponent<LineRenderer>();
        edge = obj.GetComponent<EdgeCollider2D>();
        drawnObj = obj.GetComponent<DrawnObject>();

        SetLineEdge();
    }

    void SetLineEdge()
    {
        positions.Add(edge.transform.InverseTransformPoint(lastPos));
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, lastPos);
        edge.points = positions.ToArray();
    }
}

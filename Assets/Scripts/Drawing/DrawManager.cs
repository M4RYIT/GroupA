using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum defining drawn object types
public enum DrawnObjectType
{
    Gravity,
    NoGravity
}

//Creates and sets drawbars
//Keeps the references to the other objects related to drawing mechanics
//Manages draw time consumption and drawbars switch
public class DrawManager : MonoBehaviour
{
    public KeyCode Key;
    public Transform DrawBarsParent;
    public List<DrawAsset> DrawAssets;
    public GameObject DrawBarPrefab;

    Dictionary<int, DrawRef> drawRefs = new Dictionary<int, DrawRef>();
    int index = 0;
    bool active = false;

    public float CurrentDrawTime => drawRefs[index].CurrentDrawTime;
    public bool Active { get => active; set => active = value; }

    public GameObject DrawnObjectPrefab => drawRefs[index].DrawnObjectPrefab;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(Key)) index = (index + 1) & drawRefs.Count;

        if (active) SetDrawBar(index, -Time.deltaTime);
    }

    public void SetDrawBar(int i, float delta)
    {
        DrawRef drawRef = drawRefs[i];
        drawRef.CurrentDrawTime = Mathf.Clamp(drawRef.CurrentDrawTime + delta, 0f, drawRef.MaxDrawTime);
        drawRef.DrawBar.Set(drawRef.CurrentDrawTime);
    }

    void Init()
    {
        foreach (var asset in DrawAssets)
        {
            GameObject barObj = Instantiate(DrawnObjectPrefab, DrawBarsParent);

            DrawBar bar = barObj.GetComponent<DrawBar>();
            bar.Init(asset.Color, asset.DrawBarDuration, new Vector3(1 - 2 * (int)asset.ObjectType, 1f, 1f));

            asset.Prefab.GetComponent<DrawnObject>().Init(asset.Color, asset.ObjDuration);

            drawRefs[(int)asset.ObjectType] = new DrawRef(asset.Prefab, bar, asset.DrawBarDuration);
        }
    }

    //Inner class to keep references to the parameters needed for drawing mechanics
    class DrawRef
    {
        public readonly GameObject DrawnObjectPrefab;
        public readonly DrawBar DrawBar;
        public readonly float MaxDrawTime;
        public float CurrentDrawTime;

        public DrawRef(GameObject prefab, DrawBar bar, float maxtime)
        {
            DrawnObjectPrefab = prefab;
            DrawBar = bar;
            CurrentDrawTime = MaxDrawTime = maxtime;
        }
    }
}

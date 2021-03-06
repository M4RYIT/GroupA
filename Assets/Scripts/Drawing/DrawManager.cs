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

    void Start()
    {
        GameManager.Instance.OnCollect += SetDrawBar;
        GameManager.Instance.OnPlayerDeath += DestroyDrawnObjects;
        GameManager.Instance.OnPlayerDeath += RestoreBars;

        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(Key)) SwitchBar();

        if (active) SetDrawBar(index, -Time.deltaTime);
    }

    public DrawnObject DrawnObject(Vector2 pos)
    {
        DrawRef drawRef = drawRefs[index];
        DrawnObject drawnObj = Instantiate(drawRef.Asset.Prefab, pos, Quaternion.identity, transform).GetComponent<DrawnObject>();
        drawnObj.Init(drawRef.Asset.Color, drawRef.Asset.ObjDuration);
        return drawnObj;
    }

    public void SetDrawBar(int i, float delta)
    {
        DrawRef drawRef = drawRefs[i];
        drawRef.CurrentDrawTime = Mathf.Clamp(drawRef.CurrentDrawTime + delta, 0f, drawRef.MaxDrawTime);
        drawRef.DrawBar.Set(drawRef.CurrentDrawTime);
    }

    void SwitchBar()
    {
        drawRefs[index].DrawBar.Select(false);

        index = (index + 1) % drawRefs.Count;

        drawRefs[index].DrawBar.Select();
    }

    void DestroyDrawnObjects()
    {
        DrawnObject[] objs = GetComponentsInChildren<DrawnObject>();        
        for (int i = objs.Length - 1; i>=0; i--)
        {
            Destroy(objs[i].gameObject);
        }
    }

    void RestoreBars()
    {
        foreach (var r in drawRefs.Values)
        {
            r.Reset();
        }
    }

    void Init()
    {
        foreach (var asset in DrawAssets)
        {
            DrawBar bar = Instantiate(DrawBarPrefab, DrawBarsParent).GetComponent<DrawBar>();
            bar.Init(asset.Color, asset.DrawBarDuration, new Vector3(1 - 2 * (int)asset.ObjectType, 1f, 1f));

            drawRefs[(int)asset.ObjectType] = new DrawRef(asset, bar);
        }

        drawRefs[index].DrawBar.Select();
    }

    //Inner class to keep references to the parameters needed for drawing mechanics
    class DrawRef
    {
        public DrawAsset Asset;
        public readonly DrawBar DrawBar;
        public float CurrentDrawTime;

        public float MaxDrawTime => Asset.DrawBarDuration;

        public DrawRef(DrawAsset asset, DrawBar bar)
        {
            Asset = asset;
            DrawBar = bar;
            CurrentDrawTime = asset.DrawBarDuration;
        }

        public void Reset()
        {
            CurrentDrawTime = MaxDrawTime;
            DrawBar.Set(CurrentDrawTime);
        }
    }
}

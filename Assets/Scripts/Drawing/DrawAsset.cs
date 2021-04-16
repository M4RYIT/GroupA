using UnityEngine;

//Asset defining properties needed for drawing mechanics

[CreateAssetMenu(fileName = "DrawAsset", menuName = "Drawing")]
public class DrawAsset : ScriptableObject
{
    public DrawnObjectType ObjectType;
    public GameObject Prefab;
    public Color Color;
    public float ObjDuration;
    public float DrawBarDuration;
}

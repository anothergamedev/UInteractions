using UnityEngine;

public class Item : ScriptableObject
{
    public string Name;
    public int MaxPerSlot;
    public ItemTypes Type;
    public Effects Effect;
    public Sprite NormalSprite;
    public Sprite HighlightedSprite;
    public GameObject Prefab;
}

public enum ItemTypes
{
    Cube
}

public enum Effects
{
    CubeEffect
}
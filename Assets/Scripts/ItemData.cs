using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { melee, range, gloves, shoes, heal}
    
    [Header("main info")] 
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
    public ItemType itemType;

    [Header("level data")] 
    public float baseDmg;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("weapon")] 
    public GameObject projectiles;
}

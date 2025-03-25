using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        name = "Gear " + data.itemName;
        transform.parent = GameManager.Instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.gloves:
                RateUp();
                break;
            case ItemData.ItemType.shoes:
                SpeedUp();
                break;
            default:
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon w in weapons)
        {
            switch (w.id)
            {
                case 0:
                    w.speed = 150 + (150 * rate);
                    break;
                default:
                    w.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3;
        GameManager.Instance.player.speed = speed + speed * rate;
    }
    
}

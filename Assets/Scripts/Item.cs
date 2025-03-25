using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    private Image _icon;
    private Text _textLevel;

    private void Awake()
    {
        _icon = GetComponentsInChildren<Image>()[1];
        _icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        _textLevel = texts[0];
    }

    private void LateUpdate()
    {
		if (level == data.damages.Length)
        {
            _textLevel.text = "Lv.Max";
        } else {
			_textLevel.text = "Lv." + (level + 1);
		}
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.melee:
            case ItemData.ItemType.range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDmg = data.baseDmg;
                    int nextCnt = 0;

                    nextDmg += data.baseDmg * data.damages[level];
                    nextCnt += data.counts[level];
                    
                    weapon.LevelUp(nextDmg, nextCnt);
                }
                level++;
                break;
            case ItemData.ItemType.gloves:
            case ItemData.ItemType.shoes:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.heal:
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                break;
            default:
                break;
        }
        
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

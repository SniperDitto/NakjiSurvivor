using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;

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
        _textLevel.text = "Lv." + (level + 1);
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
                break;
            case ItemData.ItemType.gloves:
                break;
            case ItemData.ItemType.shoes:
                break;
            case ItemData.ItemType.heal:
                break;
            default:
                break;
        }

        level++;
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

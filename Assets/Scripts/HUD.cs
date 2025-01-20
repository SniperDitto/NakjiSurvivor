using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        KillCnt,
        Time,
        Health,
    }
    public InfoType type;
    
    Text _text;
    Slider _slider;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {   case InfoType.Exp:
                break;
            case InfoType.Level:
                break;
            case InfoType.KillCnt:
                break;
            case InfoType.Time:
                break;
            case InfoType.Health:
                break;
            default:
                break;
        }
    }
}

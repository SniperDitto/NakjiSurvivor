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
                float currentExp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level];
                _slider.value = currentExp / maxExp;
                break;
            case InfoType.Level:
                _text.text = string.Format("Lv.{0:D}", GameManager.Instance.level);
                break;
            case InfoType.KillCnt:
                _text.text = string.Format("{0:D}", GameManager.Instance.killCnt);
                break;
            case InfoType.Time:
                float remainTime = GameManager.Instance.gameOverTime - GameManager.Instance.gameTime;
                int minute = Mathf.FloorToInt(remainTime / 60);
                int second = Mathf.FloorToInt(remainTime % 60);
                _text.text = string.Format("{0:D2}:{1:D2}", minute, second);
                break;
            case InfoType.Health:
                float currentHp = GameManager.Instance.health;
                float maxHp = GameManager.Instance.maxHealth;
                _slider.value = currentHp / maxHp;
                break;
            default:
                break;
        }
    }
}

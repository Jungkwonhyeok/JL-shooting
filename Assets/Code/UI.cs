using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public enum InfoType { Score, Level, Boss, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Score:
                
                
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", Player.power);
                break;
            case InfoType.Boss:
                float BossHealth = Enemy.Bhealth;
                float BossmaxHealth = Enemy.MaxBhealth;
                mySlider.value = BossHealth / BossmaxHealth;
                break;
            case InfoType.Time:
                
                break;
            case InfoType.Health:
                float curHealth = Player.health;
                float maxHealth = Player.maxhealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}

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
                myText.text = string.Format("SCORE : {0:n0}", Player.instance.score);

                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", Player.instance.power);
                break;
            case InfoType.Boss:
                if (Enemy.Instance == null) return;

                float BossHealth = Enemy.Instance.Bhealth;
                float BossmaxHealth = Enemy.Instance.MaxBhealth;
                mySlider.value = BossHealth / BossmaxHealth;
                if(BossHealth <= 0)
                {
                    gameObject.SetActive(false);
                }
                break;
            case InfoType.Time:
                float curTime = Player.instance.gameTime;
                float maxTime = Player.instance.maxgameTime;
                mySlider.value = curTime / maxTime;
                break;
            case InfoType.Health:
                float curHealth = Player.instance.health;
                float maxHealth = Player.instance.maxhealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}

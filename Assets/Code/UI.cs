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
               
                break;
            case InfoType.Boss:
                
                break;
            case InfoType.Time:
                
                break;
            case InfoType.Health:
               
                break;
        }
    }
}

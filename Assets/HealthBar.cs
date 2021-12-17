using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        if (text != null)
        {
            text.text = health.ToString();
        }
    }
    public void setHealth(int health)
    {
        slider.value = health;
        text.text = health.ToString();
    }
}

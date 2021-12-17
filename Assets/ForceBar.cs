using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    public void setMaxForce(float velocity)
    {
        slider.maxValue = velocity;
        slider.value = velocity;
    }
    public void setForce(float velocity)
    {
        slider.value = velocity;
        text.text = velocity.ToString("0.00") + "m/s";
    }
    // public void setForceText(float force)
    // {
    //     forceText.text = force.ToString() + "m/s";
    // }
}

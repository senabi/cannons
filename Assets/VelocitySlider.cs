using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocitySlider : MonoBehaviour
{

    [SerializeField] private Slider _slider;
    [SerializeField] private Text _sliderText;
    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = "Velocidad\nInicial     \n" + v.ToString("0.0") + " m/s";
        });
    }
    public void setVelocityVal(float velocity)
    {
        _slider.value = velocity;
        // text.text = velocity.ToString("0.00") + "m/s";
    }

    public float GetValue()
    {
        return _slider.value;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

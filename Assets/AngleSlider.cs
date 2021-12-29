using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _sliderText;
    [SerializeField] private Transform _barrelPivot;
    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = "Angulo    \n" + v.ToString();
            // _barrelPivot.rotation.
            // Debug.Log("x: " + _barrelPivot.rotation.x + " " + "y: " + _barrelPivot.rotation.y + " " + "z: "  + _barrelPivot.rotation.z);
            // Debug.Log(_barrelPivot.rotation.ToString());
            _barrelPivot.localEulerAngles = new Vector3(-v, _barrelPivot.localRotation.y, _barrelPivot.localRotation.z);
            // _barrelPivot.localRotation = Quaternion.Euler(-v, _barrelPivot.localRotation.y, _barrelPivot.localRotation.z);
            // _barrelPivot.eulerAngles = Vector3();
        });
    }

    public void setAngleVal(float angle)
    {
        _slider.value = angle;
        // text.text = velocity.ToString("0.00") + "m/s";
    }
    // Update is called once per frame
    void Update()
    {

    }
}

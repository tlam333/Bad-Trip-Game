using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Bar_behaviour : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void set_value(int n) {
        slider.value = n;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void set_max(int n) {
        slider.maxValue = n;
        slider.value = n;
        fill.color = gradient.Evaluate(1f);
    }

}

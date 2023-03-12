using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxHealth(int max)
    {
        this.slider.maxValue = max;
        this.slider.value = max;
        this.fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        Debug.Log(health);
        this.slider.value = health;
        this.fill.color = this.gradient.Evaluate(this.slider.normalizedValue);
    }
}

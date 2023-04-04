using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Controls the health bar UI
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    //sets the max health  to fill the health bar
    public void SetMaxHealth(int max)
    {
        this.slider.maxValue = max;
        this.slider.value = max;
        this.fill.color = gradient.Evaluate(1f);
    }
    //used to adjust the health as damage is taken
    public void SetHealth(int health)
    {
        Debug.Log(health);
        this.slider.value = health;
        this.fill.color = this.gradient.Evaluate(this.slider.normalizedValue);
    }
}

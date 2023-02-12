using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //This is the script that's used for each healthbar to fill it
    //Depending on how uch health is left and to define it's color

    public Slider slider;
    public Gradient gradient;

    public Image fill;

    public void SetMaxHealth(int health) 
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health) 
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}

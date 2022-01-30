using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider healthSlider;
    public player player;

    private void Start()
    {
        healthSlider.maxValue = player.maxPlayerHealth;
        healthSlider.value = player.maxPlayerHealth;
    }
    private void Update()
    {
        healthSlider.value = player.currentHealth;
    }

}

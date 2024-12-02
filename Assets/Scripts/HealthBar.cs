using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    [SerializeField] private PlayerStats playerStats;

    private float lerpSpeed = 0.05f;
    void Update()
    {
        if (healthSlider.value != playerStats.health)
        {
            healthSlider.value = playerStats.health;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, playerStats.health, lerpSpeed);
        }
    }

    /// <summary>
    /// updates the health bar slider values with current max HP
    /// </summary>
    /// <param name="maxHealth">max health of player</param>
    public void UpdateHealthBar(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        easeHealthSlider.maxValue = maxHealth;
    }
}

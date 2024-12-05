using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour // I feel like I could make the fuelbar/healthbar one script and call it bar slider or something along those lines.
{
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private Jetpack jetpack;

    void Start()
    {
        fuelSlider.maxValue = jetpack.maxFuel;
    }
    private void Update()
    {
        if (fuelSlider.value != jetpack.fuel)
        {
            fuelSlider.value = jetpack.fuel;
        }
    }
}

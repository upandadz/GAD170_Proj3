using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectedPlantVisual : MonoBehaviour
{

    private Plant plant;
    private GameObject selectedVisual;
    void Start()
    {
        plant = GetComponent<Plant>();
        selectedVisual = transform.GetChild(1).gameObject;
        PlayerActions.Instance.OnSelectedPlantChanged += PlayerActions_OnSelectedPlantChanged;
    }

    void PlayerActions_OnSelectedPlantChanged(object sender, PlayerActions.OnSelectedPlantChangedEventArgs e)
    {
        if (e.selectedPlant == plant)
        {
            Show(); // currently not activating
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        selectedVisual.SetActive(true);
    }

    void Hide()
    {
        selectedVisual.SetActive(false);
    }
}

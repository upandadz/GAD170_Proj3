using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectedPlantPotVisual : MonoBehaviour
{

    private PlantPot plantPot;
    private GameObject selectedVisual;
    void Start()
    {
        plantPot = GetComponent<PlantPot>();
        selectedVisual = transform.GetChild(1).gameObject;
        PlayerActions.Instance.OnSelectedPlantPotChanged += PlayerActionsOnSelectedPlantPotPotChanged;
    }

    void PlayerActionsOnSelectedPlantPotPotChanged(object sender, PlayerActions.OnSelectedPlantPotChangedEventArgs e)
    {
        if (e.SelectedPlantPot == plantPot)
        {
            Show(); 
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

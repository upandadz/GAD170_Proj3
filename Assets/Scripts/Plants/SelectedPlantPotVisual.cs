using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectedPlantPotVisual : MonoBehaviour
{

    private PlantPot plantPot;
    [SerializeField] private GameObject selectedVisual;
    void Start()
    {
        plantPot = GetComponent<PlantPot>();
        Player.Instance.OnSelectedPlantPotChanged += PlayerActionsOnSelectedPlantPotPotChanged;
    }

    void PlayerActionsOnSelectedPlantPotPotChanged(object sender, Player.OnSelectedPlantPotChangedEventArgs e)
    {
        if (e.SelectedPlantPot == plantPot && selectedVisual != null)
        {
            Show(); 
        }
        else if (selectedVisual != null)
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

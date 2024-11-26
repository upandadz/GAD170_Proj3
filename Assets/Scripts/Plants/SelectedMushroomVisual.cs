using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMushroomVisual : MonoBehaviour
{
    private Mushroom mushroom;
    private GameObject selectedVisual;
    void Start()
    {
        mushroom = GetComponent<Mushroom>();
        selectedVisual = transform.GetChild(0).gameObject;
        Player.Instance.OnSelectedMushroomChanged += PlayerActionsOnSelectedMushroomChanged;
    }

    void PlayerActionsOnSelectedMushroomChanged(object sender, Player.OnSelectedMushroomChangedEventArgs e)
    {
        if (e.SelectedMushroom == mushroom)
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

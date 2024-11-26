using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMushroomVisual : MonoBehaviour
{
    private Mushroom mushroom;
    [SerializeField] private GameObject selectedVisual;
    void Start()
    {
        mushroom = GetComponent<Mushroom>();
        Player.Instance.OnSelectedMushroomChanged += PlayerActionsOnSelectedMushroomChanged;
    }

    void PlayerActionsOnSelectedMushroomChanged(object sender, Player.OnSelectedMushroomChangedEventArgs e)
    {
        if (e.SelectedMushroom == mushroom && selectedVisual != null)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mushroom script attatched to all types of mushrooms
/// </summary>
public class Mushroom : MonoBehaviour
{
    private Transform pickupPoint;
    private Transform dropPoint;
    public bool pickedUp = false;
    public MushroomValue mushroomValue;
    private AudioManager audioManager;
    
    private MeshRenderer meshRenderer;
    [SerializeField] private Material rareMaterial;
    
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (GetComponent<PoisonousMushroom>() != null)
        {
            mushroomValue = new MushroomValue(60);
        }
        else if (GetComponent<MysticalMushroom>() != null)
        {
            mushroomValue = new MushroomValue(40);
        }
        else if (GetComponent<AngryMushroom>() != null)
        {
            mushroomValue = new MushroomValue(50);
        }
        meshRenderer = GetComponent<MeshRenderer>();
        if (mushroomValue.isRare)
        {
            meshRenderer.material = rareMaterial;
        }
    }
    public void Pickup(Player player)
    {
        pickedUp = true;
        player.holdingObject = true;
        pickupPoint = player.pickupPoint;
        gameObject.transform.SetParent(pickupPoint);
        gameObject.transform.position = pickupPoint.position;
        
        TryGetComponent(out AngryMushroom angryMushroom);
        TryGetComponent(out MysticalMushroom mysticalMushroom);
        if (angryMushroom != null)
        {
            angryMushroom.OnPickup();
        }
        else if (mysticalMushroom != null)
        {
            for (int i = 0; i < player.playerVisuals.Count; i++)
            {
                mysticalMushroom.OnPickup(player.playerVisuals[i]);
                audioManager.PlaySoundLoop(player.GetComponent<AudioSource>(), audioManager.audioClips[3]);
            }
        }
    }

    public void Drop(Player player)
    {
        pickedUp = false;
        player.holdingObject = false;
        dropPoint = player.selectedPlantPot.spawnPos;
        gameObject.transform.SetParent(dropPoint);
        gameObject.transform.position = dropPoint.position;
        if (GetComponent<PoisonousMushroom>() != null)
        {
            GetComponent<PoisonousMushroom>().damaging = false;
        }
        else if (GetComponent<MysticalMushroom>() != null)
        {
            for (int i = 0; i < player.playerVisuals.Count; i++)
            {
                GetComponent<MysticalMushroom>().OnDrop(player.playerVisuals[i]);
                audioManager.StopSound(player.GetComponent<AudioSource>());
            }
        }
    }
}

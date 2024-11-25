using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mushroom : MonoBehaviour
{
    private Transform pickupPoint;
    private Transform dropPoint;
    public Player player;
    public bool pickedUp = false;
    public MushroomValue mushroomValue;
    
    private MeshRenderer meshRenderer;
    [SerializeField] private Material rareMaterial;
    
    public UnityEvent OnMushroomPickedUp;
    
    void Start()
    {
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
        pickupPoint = GameObject.Find("PickupPoint").transform; // wouldn't work for multiplayer obviously but here we are
        player = pickupPoint.parent.GetComponent<Player>();
    }
    public void Pickup()
    {
        pickedUp = true;
        player.holdingObject = true;
        
        gameObject.transform.SetParent(pickupPoint);
        gameObject.transform.position = pickupPoint.position;
    }

    public void Drop()
    {
        pickedUp = false;
        player.holdingObject = false;
        dropPoint = player.selectedPlantPot.transform.GetChild(2);
        gameObject.transform.SetParent(dropPoint);
        gameObject.transform.position = dropPoint.position;
        if (GetComponent<PoisonousMushroom>() != null)
        {
            GetComponent<PoisonousMushroom>().damaging = false;
        }
    }
}

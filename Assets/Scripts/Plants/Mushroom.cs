using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Transform pickupPoint;
    private Transform dropPoint;
    public Player player;
    public bool pickedUp = false;
    public PlayerStats playerStats;

    void Start()
    {
        pickupPoint = GameObject.Find("PickupPoint").transform; // wouldn't work for multiplayer obviously but here we are
        player = pickupPoint.parent.GetComponent<Player>();
    }
    public void Pickup()
    {
        pickedUp = true;
        player.holdingObject = true;
        
        gameObject.transform.SetParent(pickupPoint);
        playerStats = GetComponentInParent<PlayerStats>();
        gameObject.transform.position = pickupPoint.position;
    }

    public void Drop()
    {
        pickedUp = false;
        player.holdingObject = false;
        dropPoint = player.selectedPlantPot.transform.GetChild(2);
        gameObject.transform.SetParent(dropPoint);
        gameObject.transform.position = dropPoint.position;
        
    }
}

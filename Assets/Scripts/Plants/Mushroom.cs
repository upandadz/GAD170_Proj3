using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Transform pickupPoint;
    public Player player;
    private Rigidbody rigidbody;
    public bool pickedUp = false;
    public PlayerStats playerStats;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        pickupPoint = GameObject.Find("PickupPoint").transform; // wouldn't work for multiplayer obviously but here we are
        player = pickupPoint.parent.GetComponent<Player>();
    }
    public void Pickup()
    {
        pickedUp = true;
        player.holdingObject = true;
        rigidbody.useGravity = false;
        gameObject.transform.SetParent(pickupPoint);
        playerStats = GetComponentInParent<PlayerStats>();
        gameObject.transform.position = pickupPoint.position;
    }

    public void Drop()
    {
        pickedUp = false;
        player.holdingObject = false;
        rigidbody.useGravity = true;
    }
}

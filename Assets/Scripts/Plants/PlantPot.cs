using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantPot : MonoBehaviour
{
    private Vector3 spawnPos;
    private GameObject mushroom;
    private Prefabs mushroomPrefabs;
    
    public enum PotType {Spawner, Collecter}
    public PotType currentPotType;

    private float raycastDistance = 2f;

    private void Start()
    {
        if (currentPotType == PotType.Spawner)
        {
            SpawnMushroom();
        }
    }

    public void Interact()
    {
        // Debug.Log("Interact");
        if (currentPotType == PotType.Spawner)
        {
            float raycastDistance = 2f;
            Physics.Raycast(transform.position, transform.up, out RaycastHit hit, raycastDistance);
            if (hit.collider != null && hit.collider.tag == "Mushroom")
            {
                GameObject mushroomHit = hit.collider.gameObject;
                if (FindObjectOfType<Player>().holdingObject == false)
                {
                    mushroomHit.GetComponent<Mushroom>().Pickup();
                }
            }
        }
        else if (currentPotType == PotType.Collecter)
        {
            Mushroom playerHeldMushroom = FindObjectOfType<Player>().transform.GetChild(0).GetChild(0).GetComponent<Mushroom>(); // this is gross to me... but it works
            if (playerHeldMushroom != null && !HasMushroom())
            {
                playerHeldMushroom.Drop();
            }
        }
    }

    void SpawnMushroom()
    {
        mushroomPrefabs = FindObjectOfType<Prefabs>();
        spawnPos = transform.GetChild(2).position; 
        mushroom = mushroomPrefabs.mushroomList[Random.Range(0, mushroomPrefabs.mushroomList.Count)];
        //Debug.Log(mushroom.name);
        Instantiate(mushroom, spawnPos, Quaternion.identity);
    }

    /// <summary>
    /// checks to see if the pot already has a mushroom on it
    /// </summary>
    /// <returns></returns>
    public bool HasMushroom()
    {
        Physics.Raycast(transform.position, transform.up, out RaycastHit hit, raycastDistance);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

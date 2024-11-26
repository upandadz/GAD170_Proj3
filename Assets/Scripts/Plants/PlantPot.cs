using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantPot : MonoBehaviour
{
    public Transform spawnPos;
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

    public void Interact(Player player)
    {
        //picking up mushroom
        if (currentPotType == PotType.Spawner)
        {
            float raycastDistance = 2f;
            Physics.Raycast(transform.position, transform.up, out RaycastHit hit, raycastDistance);
            
            //
            if (hit.collider != null && hit.collider.CompareTag("Mushroom"))
            {
                GameObject mushroomHit = hit.collider.gameObject;
                if (FindObjectOfType<Player>().holdingObject == false)
                {
                    mushroomHit.GetComponent<Mushroom>().Pickup();
                }
            }
        }
        
        //dropping off mushroom
        else if (currentPotType == PotType.Collecter)
        {
            Mushroom playerHeldMushroom = player.GetComponentInChildren<Mushroom>();
            if (playerHeldMushroom != null && !HasMushroom())
            {
                playerHeldMushroom.Drop();
            }
        }
    }

    void SpawnMushroom()
    {
        mushroomPrefabs = FindObjectOfType<Prefabs>();
        mushroom = mushroomPrefabs.mushroomList[Random.Range(0, mushroomPrefabs.mushroomList.Count)];
        //Debug.Log(mushroom.name);
        Instantiate(mushroom, spawnPos.position, Quaternion.identity);
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

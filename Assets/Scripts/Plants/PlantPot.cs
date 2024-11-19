using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantPot : MonoBehaviour
{
    private Vector3 spawnPos;
    private GameObject mushroom;
    private Prefabs mushroomPrefabs;

    private void Start()
    {
        mushroomPrefabs = FindObjectOfType<Prefabs>();
        spawnPos = transform.GetChild(2).position; 
        mushroom = mushroomPrefabs.mushroomList[Random.Range(0, mushroomPrefabs.mushroomList.Count)];
        //Debug.Log(mushroom.name);
        Instantiate(mushroom, spawnPos, Quaternion.identity);
    }

    public void Interact()
    {
        Debug.Log("Interact");
        // if pot plant has mushroom on top, maybe use raycast up . raycast hit . get component
        Physics.Raycast(transform.position, transform.up, out RaycastHit hit, Mathf.Infinity);
        if (hit.collider != null && hit.collider.tag == "Mushroom")
        {
            hit.collider.gameObject.SetActive(false);
        }
    }
}

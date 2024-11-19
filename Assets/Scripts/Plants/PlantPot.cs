using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantPot : MonoBehaviour
{
    private Vector3 spawnPos;
    private GameObject mushroom;
    private MushroomPrefabs mushroomPrefabs;

    private void Start()
    {
        mushroomPrefabs = FindObjectOfType<MushroomPrefabs>();
        spawnPos = transform.GetChild(2).position;
        mushroom = mushroomPrefabs.mushroomList[Random.Range(0, mushroomPrefabs.mushroomList.Count)];
        Debug.Log(mushroom.name);
        Instantiate(mushroom, spawnPos, Quaternion.identity);
    }

    public void Interact()
    {
        Debug.Log("Interact");
    }
}

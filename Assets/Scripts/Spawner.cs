using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Prefabs prefabs;
    public List<Transform> potSpawnPoints;
    public List<Transform> thornSpawnPoints;

    private int howManyPots = 4;
    private int howManyThorns = 4;
    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < howManyPots; i++)
        {
            // want to check to see if something has already spawned there, maybe a raycast?
            Instantiate(prefabs.plantPot, potSpawnPoints[Random.Range(0, potSpawnPoints.Count)].position, Quaternion.identity);
        }

        for (int i = 0; i < howManyThorns; i++)
        {
            Instantiate(prefabs.thorns, thornSpawnPoints[Random.Range(0, thornSpawnPoints.Count)].position, Quaternion.identity);
        }
    }
}

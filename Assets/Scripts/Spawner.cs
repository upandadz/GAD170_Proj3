using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Prefabs prefabs;
    public List<Transform> potSpawnPoints;
    public List<Transform> thornSpawnPoints;
    private float potHeight = 0.5f;

    private int howManyPots = 4;
    void Start()
    {
        for (int i = 0; i < howManyPots; i++)
        {
            Instantiate(prefabs.plantPot, potSpawnPoints[Random.Range(0, potSpawnPoints.Count)].position, Quaternion.identity);
        }

        for (int i = 0; i < thornSpawnPoints.Count; i++)
        {
            Instantiate(prefabs.thorns, thornSpawnPoints[i].position, Quaternion.identity);
        }
    }
}

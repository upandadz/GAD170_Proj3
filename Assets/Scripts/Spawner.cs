using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// spawns pots and thorns
/// </summary>
public class Spawner : MonoBehaviour
{
    public Prefabs prefabs;
    public List<Transform> potSpawnPoints;
    public List<Transform> thornSpawnPoints;

    private List<int> usedSpawnPoints = new List<int>();

    private int howManyPots = 4;
    private int howManyThorns = 4;
    public void Spawn()
    {
        usedSpawnPoints.Clear();
        for (int i = 0; i < howManyPots; i++)
        {
            int spawnPosition = Random.Range(0, potSpawnPoints.Count);
            if (usedSpawnPoints.Contains(spawnPosition))
            {
                i--; // makes sure the code can run again to check for another spawn point if already used
            }
            else
            {
                usedSpawnPoints.Add(spawnPosition);
                Instantiate(prefabs.plantPot, potSpawnPoints[spawnPosition].position, Quaternion.identity);
            }
        }

        for (int i = 0; i < howManyThorns; i++)
        {
            Instantiate(prefabs.thorns, thornSpawnPoints[Random.Range(0, thornSpawnPoints.Count)].position, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Transform roundStartPoint;
    private PlayerStats playerStats;

    public int roundNumber = 1;
    public float roundTime = 30f;
    private bool roundStarted = false;
    public int funds;
    
    public PlantPot[] allPlantPotsFound;

    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (roundStarted)
        {
            roundTime -= Time.deltaTime;
        }
    }
    public void PlayerDied()
    {
        player.frozen = true;
        // show you died UI
    }

    public void RoundOver()
    {
        // reset start position
        player.transform.position = roundStartPoint.position;
        
        // freeze player
        player.frozen = true;
        
        // restart timer
        roundTime = 30f;
        
        // destroy spawned in pots & thorns
        allPlantPotsFound = FindObjectsOfType<PlantPot>();
        foreach (PlantPot plantPot in allPlantPotsFound)
        {
            if (plantPot.currentPotType == PlantPot.PotType.Spawner)
            {
                if (plantPot.HasMushroom())
                {
                    float rayDistance = 2f;
                    Physics.Raycast(plantPot.transform.position, player.transform.up, out RaycastHit hit, rayDistance);
                    Destroy(hit.collider.gameObject); // destroys the mushroom if it has one
                }
                GameObject actualPot = plantPot.gameObject;
                Destroy(actualPot);
            }
        }
        
        // add value of collected mushrooms
        foreach (PlantPot plantPot in allPlantPotsFound)
        {
            if (plantPot.currentPotType == PlantPot.PotType.Collecter)
            {
                funds += plantPot.GetComponentInChildren<Mushroom>().mushroomValue.mushroomValue; // should probably do some renaming here
            }
        }
        
        // take out $$ for medical bill
        while (playerStats.health < playerStats.maxHealth)
        {
            playerStats.health++;
            funds--;
        }
        
        // show available funds
        // show upgrade stats ui - once stats are upgraded click button for round start
    }

    public void RoundStart()
    {
        // spawn in pots & thorns
        // build navmesh
        // begin timer
        // unfreeze player
    }

    public void GameOver()
    {
        // freeze player
        // display TOTAL collected funds
    }

    public void StartGame()
    {
        // begin timer
        // spawn obstacles
        // disable UI
        // unfreeze player
    }
}

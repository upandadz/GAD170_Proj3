using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform roundStartPoint;
    [SerializeField] private NavMesh navMesh;
    [SerializeField] private Spawner spawner;
    [SerializeField] private UIManager uiManager;
    private PlayerStats playerStats;

    public int roundNumber = 1;
    public float roundTime = 30f;
    private bool roundStarted = false;
    public int funds;
    public int totalFunds;
    
    private PlantPot[] allPlantPotsFound;

    public UnityEvent OnRoundStart;

    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (roundStarted == true)
        {
            roundTime -= Time.deltaTime;
            if (roundTime <= 0f)
            {
                RoundOver();
            }
        }
    }
    public void PlayerDied()
    {
        player.frozen = true;
        // show you died UI
    }

    public void RoundOver()
    {
        roundStarted = false;
        
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
                    if (hit.collider != null)
                    {
                        Destroy(hit.collider.gameObject); // destroys the mushroom if it has one
                    } 
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
                if (plantPot.GetComponentInChildren<Mushroom>() != null)
                {
                    funds += plantPot.GetComponentInChildren<Mushroom>().mushroomValue.mushroomValue; // should probably do some renaming here
                    totalFunds += funds;
                }
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
        OnRoundStart.Invoke(); // spawns objects then builds navmesh
        
        // begin timer
        roundStarted = true;
        
        // unfreeze player
        player.frozen = false;
    }

    public void GameOver()
    {
        // freeze player
        player.frozen = true;
        
        // display TOTAL collected funds
    }
}

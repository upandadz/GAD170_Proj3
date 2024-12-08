using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform roundStartPoint;
    [SerializeField] private NavMesh navMesh;
    [SerializeField] private Spawner spawner;
    private UIManager uiManager;
    public PlayerStats playerStats;

    public int roundNumber = 1;
    private int roundFinish = 5;
    public float roundTime = 30f;
    private bool roundStarted = false;
    
    public int funds; // If I was to redo this I would put funds in its own script
    public int totalFunds;
    public int amountSpentOnBills;
    
    private PlantPot[] allPlantPotsFound;
    public List<Mushroom> mushroomsCollected = new List<Mushroom>();

    public UnityEvent OnRoundStart;
    public UnityEvent GameWonEvent;

    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        uiManager = FindObjectOfType<UIManager>();
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
        roundStarted = false;
    }

    private void RoundOver()
    {
        roundStarted = false;

        if (roundNumber != roundFinish)
        {
            roundNumber++;
        }
        
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
                    Mushroom mushroom = plantPot.GetComponentInChildren<Mushroom>();
                    mushroomsCollected.Add(mushroom);
                    funds += mushroom.mushroomValue.mushroomValue; // should probably do some renaming here
                    totalFunds += funds;
                    Destroy(mushroom.gameObject);
                }
            }
        }

        spawner.howManyThorns++;
        
        // take out $$ for medical bill
        amountSpentOnBills = 0;
        while (playerStats.health < playerStats.maxHealth)
        {
            playerStats.health++;
            funds--;
            amountSpentOnBills++;
            if (funds <= 0)
            {
                break;
            }
        }
        uiManager.UpdateUIText(uiManager.medicalBillsText, "You spent $", amountSpentOnBills, " on medical bills");
        if (roundNumber == roundFinish)
        {
            GameWonEvent.Invoke();
        }
        else
        {
            ContinueGame();
        }
    }

    public void RoundStart()
    {
        OnRoundStart.Invoke(); // spawns objects then builds navmesh
        
        // begin timer
        roundStarted = true;
        
        // unfreeze player
        player.frozen = false;
    }
    
    public void RestartGame()
    {
        // load scene
        SceneManager.LoadScene("GameScene");
    }

    public void ContinueGame()
    {
        // show available funds
        uiManager.UpdateUIText(uiManager.fundsText, "Funds: ", funds);

        // show upgrade stats ui - once stats are upgraded click button for round start
        uiManager.endRoundUI.SetActive(true);

        if (roundNumber == roundFinish)
        {
            roundNumber++;
        }
    }
}

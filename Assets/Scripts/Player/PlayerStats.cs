using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float health = 100f;
    public float maxHealth = 100f;
    public float fragility = 100f;

    private float fragilityDivider = 50f;
    
    private GameManager gameManager;
    private UIManager uiManager;
    
    public UnityEvent OnDeath;

    public int speedLevelCost = 20;
    public int healthLevelCost = 20;
    public int fragilityLevelCost = 20;
    public int speedLevel = 0;
    public int healthLevel = 0;
    public int fragilityLevel = 0;
    private int levelCostIncrease = 10;
    private int maxLevel = 5;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
    }
    public void DamagePlayer(float damage)
    {
        damage *= fragility / fragilityDivider;
        health -= damage;
        if (health <= 0)
        {
            OnDeath.Invoke();
        }
    }

    public void LevelSpeed()
    {
        if (gameManager.funds >= speedLevelCost && speedLevel < maxLevel)
        {
            moveSpeed += 1;
            speedLevel += 1;
            uiManager.UpdateUIText(uiManager.speedLvlText, "lvl: ", speedLevel);
            gameManager.funds -= speedLevelCost;
            speedLevelCost += levelCostIncrease;
        }
    }

    public void LevelHealth()
    {
        if (gameManager.funds >= healthLevelCost && healthLevel < maxLevel)
        {
            maxHealth += 20;
            healthLevel += 1;
            uiManager.UpdateUIText(uiManager.healthLvlText, "lvl: ", healthLevel);
            health = maxHealth;
            gameManager.funds -= healthLevelCost;
            healthLevelCost += levelCostIncrease;
        }
    }

    public void LevelFragility()
    {
        if (gameManager.funds >= fragilityLevelCost && fragilityLevel < maxLevel)
        {
            fragility -= 20f;
            fragilityLevel += 1;
            uiManager.UpdateUIText(uiManager.fragilityLvlText, "lvl: ", fragilityLevel);
            gameManager.funds -= fragilityLevelCost;
            fragilityLevelCost += levelCostIncrease;
        }
    }
}

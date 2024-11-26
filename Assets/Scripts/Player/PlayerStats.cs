using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float health = 100f;
    public float maxHealth = 100f;
    public float fragility = 100f;

    private float fragilityDivider = 50f;
    
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void DamagePlayer(float damage)
    {
        damage *= fragility / fragilityDivider;
        health -= damage;
        if (health <= 0)
        {
            gameManager.PlayerDied();
        }
    }
}

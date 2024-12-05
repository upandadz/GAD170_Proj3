using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    private Prefabs prefabs;
    private Rigidbody rigidBody;
    private GameManager gameManager;
    private Player player;
    
    public bool jetpackUnlocked = false;
    private bool flying = false;
    
    public float fuel = 3f;
    public float maxFuel = 3f;

    [SerializeField] private GameObject fuelSlider;
    [SerializeField] private Transform jetpackFlamePoint;
    [SerializeField] private GameObject jetpack;

    private void Start()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody>();
        prefabs = FindObjectOfType<Prefabs>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (jetpackUnlocked == true && player.frozen == false)
        {
            PressToFly();
            FuelManagement();
        }
    }

    private void FuelManagement()
    {
        if (flying && fuel > 0)
        {
            // minus fuel using delta time
            fuel -= Time.deltaTime;
        }
        else if(!flying && fuel < maxFuel)
        {
            // add fuel using delta time
            fuel += 0.5f * Time.deltaTime;
        }
    }

    void PressToFly()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flying = true;
        }
        else if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            rigidBody.velocity = new Vector3(0, 5, 0);
            Instantiate(prefabs.particleList[3], jetpackFlamePoint.position, Quaternion.Euler(90, 0, 0));
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            flying = false;
        }
    }

    public void UnlockJetpack()
    {
        int jetpackCost = 200;
        if (gameManager.funds >= jetpackCost && jetpackUnlocked == false)
        {
            jetpack.SetActive(true);
            jetpackUnlocked = true;
            gameManager.funds -= jetpackCost;
            // show the fuel slider
            fuelSlider.SetActive(true);
        }
    }
}

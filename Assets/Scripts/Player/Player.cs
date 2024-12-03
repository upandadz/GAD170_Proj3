using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance{ get; private set; }
    public event EventHandler<OnSelectedPlantPotChangedEventArgs> OnSelectedPlantPotChanged;
    public event EventHandler<OnSelectedMushroomChangedEventArgs> OnSelectedMushroomChanged;

    public class OnSelectedMushroomChangedEventArgs : EventArgs
    {
        public Mushroom SelectedMushroom;
    }

    public class OnSelectedPlantPotChangedEventArgs : EventArgs
    {
        public PlantPot SelectedPlantPot;
    }
    
    private PlayerStats playerStats;
    private Prefabs prefabs;
    [SerializeField] private Transform sprayPoint;
    [SerializeField] private Transform jetpackFlamePoint;
    
    [Header("Game Input")]
    [SerializeField] GameInput gameInput;

    private float playerHeight = 1.4f;
    private float playerRadius = 0.49f;
    private Rigidbody rigidBody;

    private float rotateSpeed = 10f;
    private bool isWalking;
    public bool frozen = true;

    public bool holdingObject = false;
    public bool jetpackUnlocked = false;
    
    private Vector3 lastInteractDirection;
    public PlantPot selectedPlantPot;
    public Mushroom selectedMushroom;
    
    public Transform pickupPoint;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of Player found!");
        }
        Instance = this;
    }
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        rigidBody = GetComponent<Rigidbody>();
        prefabs = FindObjectOfType<Prefabs>();
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedPlantPot != null)
        {
            selectedPlantPot.Interact(this);
        }
        else if (selectedMushroom != null)
        {
            if (selectedMushroom.GetComponent<AngryMushroom>().stunned == true)
            {
                selectedMushroom.Pickup(this);
            }
        }
    }
    
    void Update()
    {
        if (frozen == false)
        {
            Movement();
            Interact();
            SprayGas();
            if (jetpackUnlocked)
            {
                Jetpack();
            }
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    void Movement()
    { 
        float moveDistance = playerStats.moveSpeed * Time.deltaTime;
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y); // as input is detected on vector2, converting to vector3 to make sure the up keys don't make the player start flying

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            // attempting only movement on X axis
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                // can only move on the X
                moveDirection = moveDirectionX;
            }
            else
            {
                // can't move on the x
                
                // attempt only Z axis movement
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                {
                    // can only move on the Z
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    // cannot move
                }
            }
        }
        if (canMove)
        {
            // different movement option
            //rigidbody.AddForce(moveDirection * playerStats.moveSpeed);
            transform.position += moveDirection * moveDistance;
        }
        isWalking = moveDirection != Vector3.zero; // if move direction does not = zero, is walking is true

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime); // makes the player look where they are moving
    }

    void Interact()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        float interactDistance = 1.5f;

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out PlantPot plantPot))
            {
                // has plant
                if (plantPot != selectedPlantPot)
                {
                    SetSelectedPlant(plantPot);
                }
            }
            else
            {
                SetSelectedPlant(null);
            }
            // trying to pick up angered mushrooms
            if (raycastHit.transform.TryGetComponent(out Mushroom mushroom))
            {
                if (mushroom != selectedMushroom)
                {
                    SetSelectedMushroom(mushroom);
                }
            }
            else
            {
                SetSelectedMushroom(null);
            }
        }
        else
        {
            SetSelectedPlant(null);
            SetSelectedMushroom(null);
        }
        
    }

    void SetSelectedPlant(PlantPot selectedPlantPot)
    {
        this.selectedPlantPot = selectedPlantPot;
        
        OnSelectedPlantPotChanged?.Invoke(this, new OnSelectedPlantPotChangedEventArgs { SelectedPlantPot = selectedPlantPot });
    }

    void SetSelectedMushroom(Mushroom mushroom)
    {
        this.selectedMushroom = mushroom;
        
        OnSelectedMushroomChanged?.Invoke(this, new OnSelectedMushroomChangedEventArgs { SelectedMushroom = mushroom });
    }

    void SprayGas()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(prefabs.particleList[2], sprayPoint.position, gameObject.transform.rotation);
        }
    }

    void Jetpack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // if has enough fuel, increase vector 3 velocity on the Y
            rigidBody.velocity = new Vector3(0, 5, 0);
            Instantiate(prefabs.particleList[3], jetpackFlamePoint.position, Quaternion.Euler(90, 0, 0));
        }
    }
}

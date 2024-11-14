using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats playerStats;
    
    [Header("Speeds")]
    [SerializeField] private float rotateSpeed = 10f;
    
    [Header("Game Input")]
    [SerializeField] GameInput gameInput;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
    }
    
    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // as input is detected on vector2, converting to vector3 to make sure the up keys don't make the player start flying
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y); 
        rigidbody.AddForce(moveDirection * playerStats.moveSpeed);
        
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime); // makes the player look where they are moving
    }
}

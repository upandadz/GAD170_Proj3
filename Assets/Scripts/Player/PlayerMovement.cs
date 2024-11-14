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
    private float playerHeight = 1.4f;
    private float playerRadius = 0.49f;

    private bool isWalking;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
    }
    
    void Update()
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
            // different movement options
            //rigidbody.AddForce(moveDirection * playerStats.moveSpeed);
            transform.position += moveDirection * moveDistance;
        }
        isWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime); // makes the player look where they are moving
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}

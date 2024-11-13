using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    
    [Header("Game Input")]
    [SerializeField] GameInput gameInput;
    
    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * (moveSpeed * Time.deltaTime);

        
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
    }
}

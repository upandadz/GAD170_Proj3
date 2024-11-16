using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    [FormerlySerializedAs("playerMovement")] [SerializeField] PlayerActions playerActions;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("IsWalking", playerActions.IsWalking());
    }
}

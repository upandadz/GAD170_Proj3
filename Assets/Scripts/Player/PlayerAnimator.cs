using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] Player player;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("IsWalking", player.IsWalking());
    }
}

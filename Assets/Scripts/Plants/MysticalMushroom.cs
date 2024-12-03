using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticalMushroom : MonoBehaviour
{
    private Mushroom mushroom;
    [SerializeField] Material glowMaterial;
    [SerializeField] private Material normalPlayerMaterial;


    void Start()
    {
        mushroom = GetComponent<Mushroom>();
    }

    public void OnPickup(MeshRenderer playerVisuals)
    {
        playerVisuals.material = glowMaterial;
    }

    public void OnDrop(MeshRenderer playerVisuals)
    {
        playerVisuals.material = normalPlayerMaterial;
    }
}

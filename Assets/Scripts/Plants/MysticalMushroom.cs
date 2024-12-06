using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticalMushroom : MonoBehaviour
{
    private Mushroom mushroom;
    [SerializeField] Material glowMaterial;
    [SerializeField] private Material normalPlayerMaterial;
    private AudioManager audioManager;


    void Start()
    {
        mushroom = GetComponent<Mushroom>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnPickup(MeshRenderer playerVisuals)
    {
        playerVisuals.material = glowMaterial;
      //  audioManager.PlaySoundLoop(GetComponentInParent<AudioSource>(), audioManager.audioClips[3]);
    }

    public void OnDrop(MeshRenderer playerVisuals)
    {
        playerVisuals.material = normalPlayerMaterial;
      //  audioManager.StopSound(GetComponentInParent<AudioSource>());
    }
}

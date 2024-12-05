using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousMushroom : MonoBehaviour
{
    // public Material playerMaterial;

    private AudioManager audioManager;
    public bool damaging = false;
    private Mushroom mushroom;
    private Prefabs prefabs;

    void Start()
    {
        prefabs = FindObjectOfType<Prefabs>();
        mushroom = GetComponent<Mushroom>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        if (mushroom.pickedUp)
        {
            if (!damaging)
            {
                StartCoroutine(PoisonDamage());
            }
        }
    }

    private IEnumerator PoisonDamage()
    {
        damaging = true;
        yield return new WaitForSeconds(1f);
        if (damaging)
        {
            GetComponentInParent<PlayerStats>().DamagePlayer(2f);
            Instantiate(prefabs.particleList[1], gameObject.GetComponentInParent<Transform>().position, transform.rotation);
            damaging = false;
            audioManager.PlaySoundOnce(GetComponentInParent<AudioSource>(), audioManager.audioClips[2]);
        }
    }
}

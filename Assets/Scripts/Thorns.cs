using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    private float damage = 10f;
    private AudioManager audioManager;
    private Prefabs prefabs;

    void Start()
    {
        prefabs = FindObjectOfType<Prefabs>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().DamagePlayer(damage);
            Instantiate(prefabs.particleList[0], other.gameObject.transform.position, Quaternion.identity);
            audioManager.PlaySoundOnce(other.gameObject.GetComponent<AudioSource>(), audioManager.audioClips[0]);
        }
    }
}

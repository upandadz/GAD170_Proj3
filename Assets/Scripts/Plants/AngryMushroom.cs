using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryMushroom : MonoBehaviour
{
    private Mushroom mushroom;
    private float timer;

    private bool angered = false;
    
    public bool stunned = false;

    void Start()
    {
        mushroom = GetComponent<Mushroom>();
    }

    void Update()
    {
        if (mushroom.pickedUp)
        {
            timer += Time.deltaTime;
            // if timer goes above X
            // hop off player transform
            // begin animation
            // angered = true
            // start chasing player
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Thorns")
        {
            stunned = true;
        }
    }
}

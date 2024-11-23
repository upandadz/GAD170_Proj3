using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngryMushroom : MonoBehaviour
{
    private Mushroom mushroom;
    private float timer;

    private bool angered = false;
    
    public bool stunned = false;
    
    private Material originalMaterial;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mushroom = GetComponent<Mushroom>();
    }

    void Update()
    {
        if (mushroom.pickedUp && !stunned)
        {
            timer += Time.deltaTime;
            // if timer goes above X
            if (timer >= 2f)
            {
                gameObject.transform.SetParent(null);
                gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                angered = true;
            }
            // hop off player transform
            // begin animation
            // angered = true
            // start chasing player
        }

        if (!stunned && angered)
        {
            agent.enabled = true;
            agent.SetDestination(mushroom.player.transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            stunned = true;
        }
    }
}

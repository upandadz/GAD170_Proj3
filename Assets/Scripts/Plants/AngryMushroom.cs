using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngryMushroom : MonoBehaviour
{
    private Mushroom mushroom;

    public bool angered = false;
    
    public bool stunned = false;
    
    private Material originalMaterial;
    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mushroom = GetComponent<Mushroom>();
    }

    void Update()
    {
        if (angered && !stunned)
        {
            agent.SetDestination(mushroom.player.transform.position); // chase player
            gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z); // having issue where mushroom is floating, this is a bandaid fix
        }
    }

    public void OnPickup()
    {
        if (!stunned)
        {
            StartCoroutine(BeginAngered());
        }
    }

    private IEnumerator BeginAngered()
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.SetParent(null);
        gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        angered = true;
        agent.enabled = true;
        mushroom.player.holdingObject = false;
    }

    public void TurnOffAgent()
    {
        agent.enabled = false;
    }
}

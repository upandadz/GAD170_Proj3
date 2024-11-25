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
        if (angered && !stunned)
        {
            agent.SetDestination(mushroom.player.transform.position);
            gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    public void OnPickup()
    {
        StartCoroutine(BeginAngered());
    }

    private IEnumerator BeginAngered()
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.SetParent(null);
        gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        angered = true;
        agent.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngryMushroom : MonoBehaviour
{
    private Transform target;

    public bool angered = false;
    
    public bool stunned = false;
    
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material angeredMaterial;
    private MeshRenderer meshRenderer;
    public NavMeshAgent agent;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (angered && !stunned)
        {
            agent.SetDestination(target.position); // chase player
            gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z); // having issue where mushroom is floating, this is a bandaid fix
        }
    }

    /// <summary>
    /// Starts a coroutine, the mushroom waits 2 seconds then starts chasing the player
    /// </summary>
    public void OnPickup()
    {
        if (!stunned)
        {
            StartCoroutine(BeginAngered());
        }
    }

    private IEnumerator BeginAngered()
    {
        yield return new WaitForSeconds(2f);;
        GetComponentInParent<Player>().holdingObject = false;
        gameObject.transform.SetParent(null);
        gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        angered = true;
        meshRenderer.material = angeredMaterial;
        agent.enabled = true;
    }

    public void CalmDown()
    {
        agent.enabled = false;
        meshRenderer.material = originalMaterial;
    }
}

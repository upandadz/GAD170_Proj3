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
    [SerializeField] private Material rareMaterial;
    [SerializeField] private Material angeredMaterial;
    private MeshRenderer meshRenderer;
    private Mushroom mushroom;
    public NavMeshAgent agent;

    private int collisionDamage = 15;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        mushroom = GetComponent<Mushroom>();
    }

    void Update()
    {
        if (angered && !stunned)
        {
            agent.SetDestination(target.position); // chase player
            gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z); // having issue where the mushroom is floating, this is a bandaid fix
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
        if (GetComponentInParent<Player>() == null)
        {
            yield break;
        }
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
        if (mushroom.mushroomValue.isRare)
        {
            meshRenderer.material = rareMaterial;
        }
        else
        {
            meshRenderer.material = originalMaterial;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && angered)
        {
            collision.gameObject.GetComponent<PlayerStats>().DamagePlayer(collisionDamage);
            
        }
    }
}

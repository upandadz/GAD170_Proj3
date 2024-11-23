using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousMushroom : MonoBehaviour
{
    public Material playerMaterial;

    public bool damaging = false;
    private Mushroom mushroom;
    private Prefabs prefabs;

    void Start()
    {
        prefabs = FindObjectOfType<Prefabs>();
        mushroom = GetComponent<Mushroom>();
    }
    void Update()
    {
        if (mushroom.pickedUp)
        {
           // playerMaterial.color = Color.Lerp(playerMaterial.color, Color.green, Mathf.PingPong(Time.time, 1));
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
            mushroom.playerStats.DamagePlayer(2f);
            Instantiate(prefabs.poisonSplatter, gameObject.GetComponentInParent<Transform>().position,
                transform.rotation);
            damaging = false;
        }
    }
}

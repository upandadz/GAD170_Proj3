using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousMushroom : MonoBehaviour
{
    public Material playerMaterial;

    private bool damaging = false;
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
           // playerMaterial.color = new Color();
            if (!damaging)
            {
                StartCoroutine(PoisonDamage()); // only triggering once
            }
        }
    }

    private IEnumerator PoisonDamage()
    {
        damaging = true;
        yield return new WaitForSeconds(0.5f);
        mushroom.playerStats.DamagePlayer(2f);
        Instantiate(prefabs.poisonSplatter, gameObject.GetComponentInParent<Transform>().position, transform.rotation); // not working
        damaging = false;
    }
}

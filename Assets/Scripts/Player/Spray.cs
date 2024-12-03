using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{
    private GameObject parent;
    private void Start()
    {
        parent = transform.parent.gameObject;
        StartCoroutine(WaitThenDestroy());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mushroom" && other.GetComponent<AngryMushroom>() != null)
        {
            AngryMushroom mushroom = other.gameObject.GetComponent<AngryMushroom>();
            mushroom.stunned = true;
            mushroom.angered = false;
            mushroom.CalmDown();
        }
    }

    private IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(parent);
    }
}

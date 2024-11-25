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
            other.gameObject.GetComponent<AngryMushroom>().stunned = true;
            Debug.Log(other.gameObject.name);
        }
    }

    private IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(parent);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticalMushroom : MonoBehaviour
{
    private Mushroom mushroom;


    void Start()
    {
        mushroom = GetComponent<Mushroom>();
    }
}

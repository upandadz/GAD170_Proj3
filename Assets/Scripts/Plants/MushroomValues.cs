using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MushroomValues
{
    public int mushroomValue;
    public bool isRare = Random.Range(0, 5) == 0;

    public MushroomValues(int value)
    {
        isRare = isRare;
        mushroomValue = value;
        if (isRare)
        {
            mushroomValue *= 2;
        }
    }
}

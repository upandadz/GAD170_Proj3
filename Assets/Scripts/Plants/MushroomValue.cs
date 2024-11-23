using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MushroomValue
{
    public int mushroomValue;
    public bool isRare = Random.Range(0, 5) == 0;

    public MushroomValue(int value)
    {
        isRare = isRare;
        mushroomValue = value;
        if (isRare)
        {
            mushroomValue *= 2;
        }
    }
}

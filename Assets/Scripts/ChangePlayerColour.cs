using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChangePlayerColour : MonoBehaviour
{
    [SerializeField] private Material playerMaterial;
    [SerializeField] private ParticleSystem splatterParticles;

    public void ChangeColour()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        playerMaterial.SetColor("_Color", new Color(r, g, b));
    }

    public void ChangeToDefaultColour()
    {
        playerMaterial.SetColor("_Color", new Color(1, 0.58f, 0));
    }
}

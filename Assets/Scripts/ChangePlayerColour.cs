using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Changes colour of the player in the start menu
/// </summary>
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
        
        // change the splatter particle colour to match player colour
    }

    public void ChangeToDefaultColour()
    {
        playerMaterial.SetColor("_Color", new Color(1, 0.58f, 0));
    }
}

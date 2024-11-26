using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    public void PlayerDied()
    {
        player.frozen = true;
        // show you died UI
    }

    public void RoundOver()
    {
        // reset start position
        // destroy spawned in pots & thorns
        // add value of collected mushrooms
        // take out $$ for medical bill
        // show available funds
        // show upgrade stats ui - once stats are upgraded click button for round start
    }

    public void RoundStart()
    {
        // spawn in pots & thorns, begin timer
    }

    public void GameOver()
    {
        
    }

    public void StartGame()
    {
        // begin timer
        // spawn obstacles
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject startGameUI;
    [SerializeField] private GameObject endRoundUI;
    [SerializeField] private GameObject endGameUI;
  //  [SerializeField] private GameObject statsUI;
    [SerializeField] private TMP_Text timerText;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        timerText.text = gameManager.roundTime.ToString();
    }
}

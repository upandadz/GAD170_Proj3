using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject startGameUI;
    public GameObject endRoundUI;
    [SerializeField] private GameObject endGameUI;
  //  [SerializeField] private GameObject statsUI;
    [SerializeField] private TMP_Text timerText;
    
    public TMP_Text speedLvlText;
    public TMP_Text healthLvlText;
    public TMP_Text fragilityLvlText;
    public TMP_Text medicalBillsText;
    public TMP_Text fundsText;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        timerText.text = gameManager.roundTime.ToString(format: "00.00");
    }

    public void DisableUI()
    {
        startGameUI.SetActive(false);
        endRoundUI.SetActive(false);
        endGameUI.SetActive(false);
    }
    
    /// <summary>
    /// Updates a TMP text
    /// </summary>
    /// <param name="textToUpdate">UI manager text you want to update</param>
    /// <param name="newText">First chunk of text</param>
    /// <param name="newInt">What int do you wish to update</param>
    /// <param name="optionalText">Optional extra text</param>
    public void UpdateUIText(TMP_Text textToUpdate, string newText, int newInt, string optionalText = null)
    {
        textToUpdate.text = newText + newInt + optionalText;
    }
}

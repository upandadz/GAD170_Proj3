using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject startGameUI;
    public GameObject endRoundUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TMP_Text timerText;
    
    public GameObject jetpackButton;
    
    public TMP_Text speedLvlText;
    public TMP_Text healthLvlText;
    public TMP_Text fragilityLvlText;
    public TMP_Text potsPrice;
    
    public TMP_Text medicalBillsText;
    public TMP_Text fundsText;

    [Header("GameOver UI Elements")] 
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text whatHappenedText;
    [SerializeField] private TMP_Text totalFundsText;
    [SerializeField] private GameObject continueButton;
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
        gameOverUI.SetActive(false);
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

    public void PlayerDied()
    {
        // change what happened text & total funds text
        UpdateUIText(whatHappenedText, "You died! You lasted ", gameManager.roundNumber, " rounds!");
        UpdateUIText(totalFundsText, "Total funds collected: ", gameManager.totalFunds);
        
        MoveGameOverUI();
    }

    public void GameWon()
    {
        whatHappenedText.text = "Game Finished!";
        UpdateUIText(totalFundsText, "Total funds collected: ", gameManager.totalFunds);
        continueButton.SetActive(true);
        MoveGameOverUI();
    }

    /// <summary>
    /// DOTweens that animate the game over UI
    /// </summary>
    private void MoveGameOverUI()
    {
        // make the panel expand from the centre
        panel.transform.localScale = new Vector3(0, 0, 0);
        panel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
            
        // make game over text come down & bounce
        gameOverText.transform.localPosition = new Vector3(0, 1000, 0);
        gameOverText.transform.DOLocalMove(new Vector3(0, 323, 0), 0.5f, true);
        
        // make what happened text come flying in
        whatHappenedText.transform.localPosition = new Vector3(-1300, 161, 0);
        whatHappenedText.transform.DOLocalMove(new Vector3(0, 161, 0), 0.5f, true);

        // make total funds text come in from the other side
        totalFundsText.transform.localPosition = new Vector3(1300, -116, 0);
        totalFundsText.transform.DOLocalMove(new Vector3(0, -116, 0), 0.5f, true);
    }
}

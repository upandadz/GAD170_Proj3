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
        // make the panel expand from the centre
        
        // make game over text come down & bounce
        
        // change what happened text & total funds text
        
        // make what happened text come flying in
        
        // make total funds text come in from the other side
        
    }
}

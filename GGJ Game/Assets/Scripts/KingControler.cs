using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KingControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kingText;
    [SerializeField] private SOGameData gameData;

    

    public void ShowCard()
    {
        int level = GameManager.GameLevel;
        if (level > gameData.KingsCards.Length) return;

        kingText.text = gameData.KingsCards[level - 1];
    }

    private void OnTurnChange(TurnStatus status)
    {
        if (status == TurnStatus.Begin)
        {
            ShowCard();
        }
    }

    private void OnEnable()
    {
        GameManager.OnTurnChange += OnTurnChange;
    }
    private void OnDisable()
    {
        GameManager.OnTurnChange -= OnTurnChange;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AI
public class JokerController : MonoBehaviour
{
    [SerializeField] private int jokerNum;
    [SerializeField] private MemeCard myCard;

    private SOMemeCard _lastMemeCard;

    private void OnPlayerChoose()
    {
        var meme = MemeManager.Instance.GetRandomMeme();
        myCard.SetMyMeme(meme);
        GameManager.Instance.KingControler.JokerMemes[jokerNum] = myCard;
    }
    private void OnTurnChange(TurnStatus status)
    {
        if (status == TurnStatus.BeforeEnd)
        {
            MemeManager.Instance.AddUsedMeme(_lastMemeCard);
        }
    }

    
    private void OnEnable()
    {
        GameManager.OnPlayerChoose += OnPlayerChoose;
        GameManager.OnTurnChange += OnTurnChange;
    }
    private void OnDisable()
    {
        GameManager.OnPlayerChoose -= OnPlayerChoose;
        GameManager.OnTurnChange -= OnTurnChange;
    }
}

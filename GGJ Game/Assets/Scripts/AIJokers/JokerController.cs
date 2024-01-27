using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AI
public class JokerController : MonoBehaviour
{
    [SerializeField] private int jokerNum;
    [SerializeField] private MemeCard myCard;
    private void OnPlayerChoose()
    {
        var meme = MemeManager.Instance.GetRandomMeme();
        myCard.SetMyMeme(meme);
        GameManager.Instance.KingControler.JokerMemes[jokerNum] = myCard;
    }


    private void OnEnable()
    {
        GameManager.OnPlayerChoose += OnPlayerChoose;
    }
    private void OnDisable()
    {
        GameManager.OnPlayerChoose -= OnPlayerChoose;
    }
}

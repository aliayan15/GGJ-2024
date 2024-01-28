using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private MemeCard[] memeCards;

    [SerializeField] private TextMeshProUGUI scoreText;

    public int MyScore { get { return _score; } set { _score = value; scoreText.text = "Your Point: " + _score; } }
    private int _score;
    public static SOMemeCard MyChoose { get; set; }
    public static bool CanPlay { get; set; }

    private void Start()
    {
        scoreText.text = "Your Point: " + _score;
    }


    private void Update()
    {
       // pause
    }

    
    private void OnTurnChange(TurnStatus status)
    {
        if (status == TurnStatus.BeforeEnd)
        {
            MemeManager.Instance.AddUsedMeme(MyChoose);
        }
        if (status == TurnStatus.Begin)
        {
            for (int i = 0; i < memeCards.Length; i++)
            {
                memeCards[i].CheckMeme();
            }
            CanPlay = true;
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

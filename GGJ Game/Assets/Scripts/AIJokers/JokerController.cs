using DG.Tweening;
using TMPro;
using UnityEngine;

// AI
public class JokerController : MonoBehaviour
{
    [SerializeField] private int jokerNum;
    [SerializeField] private MemeCard myCard;
    [SerializeField] private float delayAfterPlayer = 0.5f;
    [SerializeField] private RectTransform myCardImage;
    [SerializeField] private TextMeshProUGUI scoreText;

    public int MyScore { get { return _score; } set { _score = value; scoreText.text = "Point: " + _score; } }
    private int _score = 0;

    private SOMemeCard _lastMemeCard;

    private void Start()
    {
        myCardImage.DOAnchorPosX(0, 0f);
        scoreText.text = "Point: " + _score;
    }

    private void ChooseCard()
    {
        var meme = MemeManager.Instance.GetRandomMeme();
        myCard.SetMyMeme(meme);
        GameManager.Instance.KingControler.JokerMemes[jokerNum] = myCard;
        // card animation
        myCardImage.DOAnchorPosX(230, 0.3f);
        _lastMemeCard = meme;
    }
    private void OnPlayerChoose()
    {
        Invoke(nameof(ChooseCard), delayAfterPlayer);
    }

    private void OnTurnEnd()
    {
        myCardImage.DOAnchorPosX(0, 0.2f);
    }

    private void OnTurnChange(TurnStatus status)
    {
        if (status == TurnStatus.BeforeEnd)
        {
            MemeManager.Instance.AddUsedMeme(_lastMemeCard);
            myCardImage.DOAnchorPosX(0, 0.2f);
        }
        if (status == TurnStatus.End)
        {
            OnTurnEnd();
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

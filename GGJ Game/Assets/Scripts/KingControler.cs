using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KingControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kingText;
    [SerializeField] private float timeBtwnChars;
    [SerializeField] private SOGameData gameData;
    [SerializeField] private float delayAfterPlayer = 2f;
    [Space(10)]
    [Tooltip("joker 1,2 player")]
    [SerializeField] private ScoreMarker[] scoreMarkers;
    [Header("Jokers")]
    [SerializeField] JokerController joker1;
    [SerializeField] JokerController joker2;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameWin;
    [SerializeField] Sprite[] kingImages;
    [SerializeField] Sprite kingSmiling;
    [SerializeField] Image kingImage;

    public MemeCard PlayerMeme { get; set; }
    [HideInInspector]
    public MemeCard[] JokerMemes = new MemeCard[2];

    public int KingKardIndex { get; private set; }
    private List<string> _kingKards = new List<string>();


    public void ShowCard()
    {
        if (_kingKards.Count == 0) // start of the game
        {
            _kingKards = gameData.KingsCards.ToList();
        }

        if (GameManager.GameLevel == 6)
        {
            Player.CanPlay = false;
            Debug.Log("Game Finished");
            Invoke(nameof(GameFinished), 0.5f);
            return;
        } // game finish

        KingKardIndex = UnityEngine.Random.Range(0, _kingKards.Count);
        string text = _kingKards[KingKardIndex];
        StartCoroutine(TextVisible(kingText, text));
        kingImage.sprite = kingImages[UnityEngine.Random.Range(0, kingImages.Length)];
        _kingKards.RemoveAt(KingKardIndex);
    }

    private void GameFinished()
    {
        // who win
        if (GameManager.Instance.player.MyScore < joker1.MyScore || GameManager.Instance.player.MyScore < joker2.MyScore)
        {
            // game over
            gameOver.SetActive(true);
        }
        else
        {
            // player win
            gameWin.SetActive(true);
        }
        GameManager.Instance.ChangeTurnStatus(TurnStatus.GameEnd);
    }

    private IEnumerator TextVisible(TextMeshProUGUI textMesh, string text, bool closeText = false)
    {
        textMesh.transform.parent.gameObject.SetActive(true);
        textMesh.text = text;
        textMesh.ForceMeshUpdate();
        int totalVisibleCharacters = textMesh.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textMesh.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                if (closeText)
                    textMesh.transform.parent.gameObject.SetActive(false);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }


    private void OnTurnChange(TurnStatus status)
    {
        if (status == TurnStatus.Begin)
        {
            ShowCard();
        }
        if (status == TurnStatus.BeforeEnd)
        {
            for (int i = 0; i < scoreMarkers.Length; i++)
            {
                scoreMarkers[i].HideScore();
            }
        }
    }

    #region Player Choose
    private void OnPlayerChoose()
    {
        StartCoroutine(TurnEndSequence());
    }
    private IEnumerator TurnEndSequence()
    {
        yield return new WaitForSeconds(delayAfterPlayer - 1f);
        string text = "Well well well...";
        StartCoroutine(TextVisible(kingText, text));
        yield return new WaitForSeconds(1.5f);
        kingImage.sprite = kingSmiling;
        int[] scores = new int[3];
        int jok1 = JokerMemes[0].GetScore();
        int jok2 = JokerMemes[1].GetScore();
        int jok3 = PlayerMeme.GetScore();
        int sc_jok1 = 0, sc_jok2 = 0, sc_jok3 = 0;

        scores[0] = jok1;
        scores[1] = jok2;
        scores[2] = jok3;
        Array.Sort(scores);
        Array.Reverse(scores);

        if (scores[0] == jok1)
        {
            sc_jok1 = 3;
        }
        else if (scores[0] == jok2)
        {
            sc_jok2 = 3;
        }
        else if (scores[0] == jok3)
        {
            sc_jok3 = 3;
        }
        ///////////////////////
        if (scores[1] == jok1)
        {
            sc_jok1 = 2;
        }
        else if (scores[1] == jok2)
        {
            sc_jok2 = 2;
        }
        else if (scores[1] == jok3)
        {
            sc_jok3 = 2;
        }
        ///////////////////////
        if (scores[2] == jok1)
        {
            sc_jok1 = 1;
        }
        else if (scores[2] == jok2)
        {
            sc_jok2 = 1;
        }
        else if (scores[2] == jok3)
        {
            sc_jok3 = 1;
        }

        scoreMarkers[0].ShowScore(sc_jok1);
        yield return new WaitForSeconds(1f);
        scoreMarkers[1].ShowScore(sc_jok2);
        yield return new WaitForSeconds(1f);
        scoreMarkers[2].ShowScore(sc_jok3);
        yield return new WaitForSeconds(2f); // waiteing for reset
        // turn end
        GameManager.Instance.ChangeTurnStatus(TurnStatus.BeforeEnd);
    }

    public void Talk(string text)
    {
        StartCoroutine(TextVisible(kingText, text));
    }

    #endregion

    private void OnEnable()
    {
        GameManager.OnTurnChange += OnTurnChange;
        GameManager.OnPlayerChoose += OnPlayerChoose;
    }
    private void OnDisable()
    {
        GameManager.OnTurnChange -= OnTurnChange;
        GameManager.OnPlayerChoose -= OnPlayerChoose;
    }
}

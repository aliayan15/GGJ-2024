using System.Collections;
using TMPro;
using UnityEngine;

public class KingControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kingText;
    [SerializeField] private float timeBtwnChars;
    [SerializeField] private SOGameData gameData;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI[] scoreTexts;

    public MemeCard PlayerMeme { get; set; }
    [HideInInspector]
    public MemeCard[] JokerMemes = new MemeCard[2];

    public int KingKardIndex { get; private set; }

    private void Start()
    {
        ShowScoreTexts(false);
    }

    public void ShowCard()
    {
        int level = GameManager.GameLevel;
        if (level > gameData.KingsCards.Length)
        {
            Debug.Log("Game Finished");
            return;
        }

        KingKardIndex = level - 1;
        string text = gameData.KingsCards[KingKardIndex];
        StartCoroutine(TextVisible(kingText, text));
    }
    private IEnumerator TextVisible(TextMeshProUGUI textMesh, string text)
    {
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
    }
    private void OnPlayerChoose()
    {
        Invoke(nameof(GiveScore), 2f);
    }
    private void GiveScore()
    {
        ShowScoreTexts(true);

        scoreTexts[0].text = PlayerMeme.GetScore().ToString();
        scoreTexts[1].text = JokerMemes[0].GetScore().ToString();
        scoreTexts[2].text = JokerMemes[1].GetScore().ToString();
    }
    private void ShowScoreTexts(bool show)
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            scoreTexts[i].transform.parent.gameObject.SetActive(show);
        }
    }

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

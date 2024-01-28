using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TurnStatus
{
    Begin,
    End,
    BeforeEnd,
    GameEnd
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static int GameLevel { get; private set; } = 1;
    public static Action<TurnStatus> OnTurnChange;
    public static Action OnPlayerChoose;
    [Header("Refs")]
    public KingControler KingControler;
    public RectTransform playerCardPlace;
    public Player player;
    [SerializeField] private GameObject[] startObjs;


    public TurnStatus CurrentTurnStatus { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("Error!", this);
    }

    private void Start()
    {
        //ChangeTurnStatus(TurnStatus.Begin);
        for (int i = 0; i < startObjs.Length; i++)
        {
            startObjs[i].SetActive(false);
        }
        StartCoroutine(StartTalk());
    }
    private IEnumerator StartTalk()
    {
        string talk = "";

        talk = "You have got one last chance!";
        KingControler.Talk(talk);
        yield return new WaitForSeconds(4f);
        talk = "You and those two jokers.";
        KingControler.Talk(talk);
        yield return new WaitForSeconds(4f);
        talk = "Only one will be left… I’m too bored…";
        KingControler.Talk(talk);
        yield return new WaitForSeconds(5f);
        talk = "Show yourself, use whatever leverage you have.";
        KingControler.Talk(talk);
        yield return new WaitForSeconds(5f);
        talk = "To… Make… Me… Laugh!";
        KingControler.Talk(talk);
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < startObjs.Length; i++)
        {
            startObjs[i].SetActive(true);
        }
        ChangeTurnStatus(TurnStatus.Begin);
    }

    public void ChangeTurnStatus(TurnStatus turnStatus)
    {
        Debug.Log("TurnStatus: " + turnStatus);
        CurrentTurnStatus = turnStatus;
        OnTurnChange?.Invoke(CurrentTurnStatus);

        if (CurrentTurnStatus == TurnStatus.End)
            OnTurnEnd();
        if (CurrentTurnStatus == TurnStatus.BeforeEnd)
            StartCoroutine(OnTurnBeforeEnd());
    }

    private IEnumerator OnTurnBeforeEnd()
    {
        yield return new WaitForSeconds(1f);
        ChangeTurnStatus(TurnStatus.End);
    }

    private void OnTurnEnd()
    {
        GameLevel++;
        ChangeTurnStatus(TurnStatus.Begin);
    }

    #region Utility
    public IEnumerator TextVisible(TextMeshProUGUI textMesh, string text, float timeBtwnChars)
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
    #endregion
}

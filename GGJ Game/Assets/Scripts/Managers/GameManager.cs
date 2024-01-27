using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TurnStatus
{
    Begin,
    End
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static int GameLevel { get; private set; } = 1;
    public static Action<TurnStatus> OnTurnChange;
    public static Action OnPlayerChoose;
    public KingControler KingControler;

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
        ChangeTurnStatus(TurnStatus.Begin);
    }

    public void ChangeTurnStatus(TurnStatus turnStatus)
    {
        CurrentTurnStatus = turnStatus;
        OnTurnChange?.Invoke(CurrentTurnStatus);

        if (CurrentTurnStatus == TurnStatus.End)
            OnTurnEnd();
    }

    private void OnTurnEnd()
    {
        GameLevel++;
        ChangeTurnStatus(TurnStatus.Begin);
    }

    #region Utility
    public IEnumerator TextVisible(TextMeshProUGUI textMesh, string text,float timeBtwnChars)
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

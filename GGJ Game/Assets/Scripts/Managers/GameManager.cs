using System;
using System.Collections;
using System.Collections.Generic;
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


}

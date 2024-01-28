using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private MemeCard[] memeCards;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pauseMenu;

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
#if !UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu(true);
        }
#else
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowPauseMenu(true);
        }
#endif
    }

    #region Pause Menu
    public void ShowPauseMenu(bool show)
    {
        pauseMenu.SetActive(show);
    }
    public void ReStart()
    {
        Debug.Log("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

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
        if (status == TurnStatus.GameEnd)
        {
            CanPlay = false;
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

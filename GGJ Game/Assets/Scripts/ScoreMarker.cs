using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMarker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform scoreImage;
    [SerializeField] JokerController jokerController;
    [SerializeField] Player player;

    private Vector3 _scele;

    private void Start()
    {
        _scele = scoreImage.localScale;
        scoreImage.localScale = Vector3.zero;
    }

    public void ShowScore(int score)
    {
        text.text = score.ToString();
        scoreImage.DOScale(_scele * 1.2f, 0.3f).OnComplete(() =>
        {
            scoreImage.DOScale(_scele, 0.2f).OnComplete(() =>
            {
                if (player != null)
                    player.MyScore += score;
                if (jokerController != null)
                    jokerController.MyScore += score;
            });
        });
    }
    public void HideScore()
    {
        scoreImage.DOScale(_scele * 0, 0.2f);
    }
}

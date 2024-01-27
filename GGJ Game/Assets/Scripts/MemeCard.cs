using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeCard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer s_renderer;

    public bool IsSelected { get; set; } = true;

    private SOMemeCard _myMeme;
    private Vector3 _myPos;
    private Vector3 _myScale;

    private void Start()
    {
        _myPos = transform.position;
        _myScale = transform.localScale;
    }

    public void CheckMeme()
    {
        if (!IsSelected) return;
        _myMeme = MemeManager.Instance.GetRandomMeme();
        SetSprite();
        IsSelected = false;
        transform.DOScale(_myScale, 0.2f);
        transform.DOMove(_myPos, 0.2f);
    }

   public void SetMyMeme(SOMemeCard meme)
    {
        _myMeme=meme;
    }

    public int GetScore()
    {
        return _myMeme.Scores[GameManager.Instance.KingControler.KingKardIndex];
    }

    private void SetSprite()
    {
        s_renderer.sprite = _myMeme.MemeSprite;
    }
    
}

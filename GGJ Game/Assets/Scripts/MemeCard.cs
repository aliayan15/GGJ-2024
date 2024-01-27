using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeCard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer s_renderer;

    public bool IsSelected { get; set; } = true;

    private SOMemeCard _myMeme;

    public void CheckMeme()
    {
        if (!IsSelected) return;
        _myMeme = MemeManager.Instance.GetRandomMeme();
        SetSprite();
        IsSelected = false;
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

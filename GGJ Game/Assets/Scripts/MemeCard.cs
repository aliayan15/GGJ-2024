using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MemeCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image cardImage;

    public bool IsSelected { get; set; } = true;

    private SOMemeCard _myMeme;
    private Vector2 _myPos;
    private Vector3 _myScale;
    private RectTransform _recTransform;
    private Quaternion _myRos;

    private void Awake()
    {
        _recTransform = GetComponent<RectTransform>();
        _myPos = _recTransform.anchoredPosition;
        _myScale = transform.localScale;
        _myRos = _recTransform.rotation;
    }

    public void CheckMeme()
    {
        if (!IsSelected) return;
        _myMeme = MemeManager.Instance.GetRandomMeme();
        if(_myMeme == null ) return;

        SetSprite();
        IsSelected = false;
        //reset transform
        _recTransform.DOScale(_myScale, 0.2f);
        _recTransform.DOAnchorPos(_myPos, 0.2f);
    }

    public void SetMyMeme(SOMemeCard meme)
    {
        _myMeme = meme;
        SetSprite();
    }
    // player choose this
    public void OnMemeSelected()
    {
        if (!Player.CanPlay) return;
        GameManager.Instance.KingControler.PlayerMeme = this;
        GameManager.OnPlayerChoose?.Invoke();
        IsSelected = true;
        // selected animation
        StartCoroutine(SelectedCardAnimation());
        Player.MyChoose = _myMeme;
        Player.CanPlay = false;
    }
    private IEnumerator SelectedCardAnimation()
    {
        _recTransform.DOAnchorPos(GameManager.Instance.playerCardPlace.anchoredPosition, 0.5f);
        _recTransform.DORotate(new Vector3(0, 0, 0), 0.2f);
        yield break;
    }

    public int GetScore()
    {
        return _myMeme.Scores[GameManager.Instance.KingControler.KingKardIndex];
    }

    private void SetSprite()
    {
        cardImage.sprite = _myMeme.MemeSprite;
    }

    private void OnTurnChange(TurnStatus status)
    {
        if (status == TurnStatus.BeforeEnd)
        {
            // reset
            _recTransform.DOAnchorPos(_myPos, 0.3f);
            _recTransform.DOScale(_myScale, 0.3f);
            _recTransform.DORotate(_myRos.eulerAngles, 0.3f);
        }
        if (status == TurnStatus.GameEnd)
        {
            this.enabled = false;
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

    #region On Hover Animation
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Player.CanPlay) return;
        transform.DOScale(_myScale * 1.1f, 0.2f);
        _recTransform.DOAnchorPosY(_myPos.y + 20f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Player.CanPlay) return;
        transform.DOScale(_myScale, 0.2f);
        _recTransform.DOAnchorPosY(_myPos.y, 0.2f);
    }
    #endregion
}

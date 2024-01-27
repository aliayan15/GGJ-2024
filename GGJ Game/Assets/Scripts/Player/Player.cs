using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MemeCard[] memeCards;
    [SerializeField] private Camera mainCam;
    [SerializeField] private LayerMask cardLayer;
    [SerializeField] private Vector3 cardScale;

    public MemeCard MyChoose { get; private set; }

    private bool _canChoose = false;
    private Collider2D _detectedCollider;
    private Collider2D _lastCollider;
    private Vector2 detectionBoxSize = new Vector2(0.2f, 0.2f);
    private float rotationAngle = 0;
    private Vector3 _lastCardPos;

    private void Start()
    {

    }


    private void Update()
    {
        if (!_canChoose) return;

        SendRay();

        if (Input.GetMouseButtonDown(0))
        {
            if (!_detectedCollider) return;
            if (_detectedCollider.TryGetComponent(out MemeCard memeCard))
            {
                _canChoose = false;
                // player choosed
                MyChoose = memeCard;
                GameManager.Instance.KingControler.PlayerMeme = MyChoose;
                GameManager.OnPlayerChoose?.Invoke();
                _detectedCollider.transform.DOScale(cardScale * 1.1f, 0.1f);
                _lastCardPos = _detectedCollider.transform.position;
                _detectedCollider.transform.DOMoveY(_lastCardPos.y + 1, 0.3f);
            }
        }
    }

    private void SendRay()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        Vector2 mouseWorldPosition = mainCam.ScreenToWorldPoint(mousePosition);

        _detectedCollider = Physics2D.OverlapBox(mouseWorldPosition, detectionBoxSize, rotationAngle, cardLayer);

        if (_detectedCollider == null)
        {
            if (_lastCollider)
            {
                _lastCollider.transform.DOScale(cardScale, 0.3f);
                _lastCollider = null;
            }

        }


        if (_detectedCollider != _lastCollider)
        {
            if (_lastCollider)
                _lastCollider.transform.DOScale(cardScale, 0.1f);
            _lastCollider = _detectedCollider;
            _detectedCollider.transform.DOScale(cardScale * 1.1f, 0.1f);
        }
    }

    private void OnTurnChange(TurnStatus status)
    {
        if (status == TurnStatus.Begin)
        {
            for (int i = 0; i < memeCards.Length; i++)
            {
                memeCards[i].CheckMeme();
            }
            _canChoose = true;
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

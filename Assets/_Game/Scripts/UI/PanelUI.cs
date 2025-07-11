using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] Image _bagroundImage;
    [SerializeField] private RectTransform _rect;

    [SerializeField] protected float _duration = 0.3f;
    [SerializeField] Vector2 _closePanel = new Vector2(0, -400);
    [SerializeField] Vector2 _openPanel;
    [SerializeField] bool _scale = true;
    [SerializeField] bool _move = true;
    [SerializeField] float _closeScale = 0;
    [SerializeField] Ease _scaleOpenEaseType = Ease.Linear;
    [SerializeField] Ease _scaleCloseEaseType = Ease.Linear;

    protected bool _open;
    protected bool _onUpdate;

    public RectTransform Rect => _rect;
    public bool isOpen => _open;

    protected float _BagroundImageFade;

    public virtual void Init()
    {
        _BagroundImageFade = (_bagroundImage) ? _bagroundImage.color.a : 0.6f;

        gameObject.SetActive(true);
        _rect.gameObject.SetActive(false);

        if (_move)
            _rect.anchoredPosition = _closePanel;


        if (_scale)
            _rect.localScale = Vector3.one * _closeScale;

        if (_bagroundImage)
        {
            _bagroundImage.DOFade(0, 0);
            _bagroundImage.raycastTarget = false;
        }
    }

    public void Open(Vector2 AtPosition)
    {
        _openPanel = AtPosition;
        Open();
    }

    [ContextMenu("Open")]
    public virtual void Open()
    {
        if (_onUpdate)
            return;
        print("Open Panel " + name);
        if (!_open)
        {
            _rect.gameObject.SetActive(true);

            if (_bagroundImage)
            {
                _bagroundImage.DOFade(_BagroundImageFade, _duration * 0.5f);
                _bagroundImage.raycastTarget = true;
            }

            if (_move)
                _rect.DOAnchorPos(_openPanel, _duration).SetId(this).OnComplete(EndOpen);
            else
            {
                DOVirtual.DelayedCall(_duration, EndOpen).SetId(this);
            }

            if (_scale)
                _rect.DOScale(1, _duration).SetEase(_scaleOpenEaseType).SetId(this);
            _open = true;
            _onUpdate = true;
        }
    }

    [ContextMenu("Close")]
    public virtual void Close()
    {
        if (_onUpdate)
            return;
        print("Close Panel " + name);
        if (_open)
        {
            DOTween.Kill(this, true);

            if (_bagroundImage)
            {
                _bagroundImage.DOFade(0, _duration * 0.5f);
                _bagroundImage.raycastTarget = false;
            }

            if (_move)
                _rect.DOAnchorPos(_closePanel, _duration).SetId(this).OnComplete(EndClose);
            else
            {
                DOVirtual.DelayedCall(_duration, EndClose).SetId(this);
            }

            if (_scale)
                _rect.DOScale(_closeScale, _duration).SetEase(_scaleCloseEaseType).SetId(this);
            _open = false;
            _onUpdate = true;
        }
    }

    void EndClose()
    {
        _rect.gameObject.SetActive(false);
        _onUpdate = false;
    }

    void EndOpen()
    {
        _onUpdate = false;
    }
}

using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int type { get; private set; } = -1;

    [SerializeField] Transform _visuleBody;
    [SerializeField] SpriteRenderer _visuleSprite;

    internal void SetType(int t)
    {
        type = t;
        _visuleSprite.sprite = CardMatrix.cartdSprites[t];
    }

    public void FlipOpen()
    {
        _visuleBody.DOKill();
        _visuleBody.DOLocalRotate(new Vector3(0, 0, 0), 0.4f);
    }

    public void FlipClose()
    {
        _visuleBody.DOKill();
        _visuleBody.DOLocalRotate(new Vector3(0, 180, 0), 0.4f);
    }

    public void FlipOpen(float delay)
    {
        _visuleBody.DOLocalRotate(new Vector3(0, 0, 0), 0.4f).SetDelay(delay);
    }

    public void FlipClose(float delay)
    {
        _visuleBody.DOLocalRotate(new Vector3(0, 180, 0), 0.4f).SetDelay(delay);
    }

    public void ToDestroy()
    {
        _visuleBody.DOScale(Vector3.one * 1.1f, 0.3f).SetDelay(0.4f).OnComplete(() =>
        {
            _visuleBody.DOScale(Vector3.zero, 0.6f);
            transform.DOLocalRotate(new Vector3(0, 0, 300), 0.6f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        });
        Destroy(gameObject, 1.33f);
    }
}

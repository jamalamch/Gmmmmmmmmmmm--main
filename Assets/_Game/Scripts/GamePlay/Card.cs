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

}

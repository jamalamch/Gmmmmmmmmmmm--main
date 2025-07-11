using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;

public class CardMatrix : MonoBehaviour
{
    public static Sprite[] cartdSprites { get; private set; }

    public Card[,] cards;

    [SerializeField] Card _cardPrefab;
    [Space]
    [SerializeField] float _widthCard = 1f;
    [SerializeField] float _heightCard = 1.3f;
    [Space]
    [SerializeField] Sprite[] _cartdSprite;
    [Space]
    [SerializeField] TouchPad _touchPad;
    public int row { get; private set; }
    public int column { get; private set; }
    public int countCard { get; private set; }

    bool _canFlipCard;
    Card _selectCard;
    int _selectI, _selectJ;
    int _currentCardCount;

    private void Awake()
    {
        cartdSprites = _cartdSprite;
    }

    public void Init(int row, int column, string data)
    {
        _currentCardCount = countCard = row * column;
        this.row = row;
        this.column = column;
        int[] cardsType = JsonConvert.DeserializeObject<int[]>(data);
        cards = new Card[row, column];
        InstantiateCard(cardsType);
    }

    public void Init(int row, int column)
    {
        _currentCardCount = countCard = row * column;

        if (countCard > 1 && countCard % 2 == 0)
        {
            this.row = row;
            this.column = column;
            cards = new Card[row, column];

            //arry with type of card sort [1,1,2,2, .. n,n]
            int[] cardsType = new int[countCard];
            for (int i = 0; i < countCard / 2; i++)
            {
                cardsType[i * 2] = cardsType[i * 2 + 1] = i;
            }
            //Shuffle arry type of card sort
            for (int i = 0; i < countCard; i++)
            {
                int temp = cardsType[i];
                int randomIndex = Random.Range(i, countCard);
                cardsType[i] = cardsType[randomIndex];
                cardsType[randomIndex] = temp;
            }
            InstantiateCard(cardsType);
        }
        else
        {
            Debug.LogError("row x column == zero ou row x column % 2 != zero -- " + countCard);
            Debug.Break();
        }
    }

    public void InstantiateCard(int[] cardsType)
    {
        //Instantiate card prefab with type

        int t = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (cardsType[t] >= 0)
                {
                    Vector3 pos = GetPositionInMatrix(i, j);
                    Card card = Instantiate(_cardPrefab, pos, Quaternion.identity, transform);
                    card.SetType(cardsType[t]);
                    cards[i, j] = card;
                }
                else
                    _currentCardCount--;
                t++;
            }
        }


        DOVirtual.DelayedCall(GameManager.instance.flipAllCardOnStartAfter, () =>
        {
            _canFlipCard = true;
            CloseAllCard();
        });
    }

    public void ResetMatrix()
    {
        _canFlipCard = false;
        _selectCard = null;
        foreach (var item in cards)
        {
            if (item)
            {
                Destroy(item.gameObject);
            }
        }
    }

    private void Update()
    {
        if (GameManager.gameStart && _canFlipCard)
        {
            if (_touchPad.Click)
            {
                GeIndexInMatrixFromPosition(_touchPad.pointerWord, out int i, out int j);
                if (i >= 0 && i < row && j >= 0 && j < column)
                {
                    Card targetSelect = cards[i, j];
                    if (targetSelect && targetSelect != _selectCard)
                    {
                        targetSelect.FlipOpen();
                        AudioManager.instance.Play("flipp");
                        if (_selectCard)
                        {
                            if (_selectCard.type == targetSelect.type)
                            {
                                _selectCard.ToDestroy();
                                targetSelect.ToDestroy();
                                cards[i, j] = null;
                                cards[_selectI, _selectJ] = null;
                                GameManager.instance.AddMatchScore();
                                _currentCardCount -= 2;
                                AudioManager.instance.PlayDelayed("match", 0.4f);
                                if (_currentCardCount <= 0)
                                {
                                    DOVirtual.DelayedCall(1.3f, () =>
                                    {
                                        GameManager.instance.GameEnd();
                                        AudioManager.instance.Play("end");
                                    });
                                }
                            }
                            else
                            {
                                _selectCard.FlipClose(0.4f);
                                targetSelect.FlipClose(0.4f);
                                GameManager.instance.AddTurnScore();
                                AudioManager.instance.PlayDelayed("nomatch", 0.4f);
                            }
                            _selectCard = null;
                        }
                        else
                        {
                            _selectCard = targetSelect;
                            _selectI = i;
                            _selectJ = j;
                        }
                    }
                    else
                    {
                        if (_selectCard)
                        {
                            GameManager.instance.AddTurnScore();
                            _selectCard.FlipClose();
                            _selectCard = null;
                        }
                    }
                }
            }
        }
    }

    void CloseAllCard()
    {
        foreach (var item in cards)
        {
            if (item)
            {
                item.FlipClose();
            }
        }
    }

    void OpenAllCard()
    {
        foreach (var item in cards)
        {
            if (item)
            {
                item.FlipOpen();
            }
        }
    }

    Vector3 GetPositionInMatrix(int i, int j)
    {
        float centerMatixX = (_widthCard * (row + 1)) / 2;
        float centerMatixY = (_heightCard * (column + 1)) / 2;
        return new Vector2(_widthCard * (i + 1) - centerMatixX, _heightCard * (j + 1) - centerMatixY);
    }

    void GeIndexInMatrixFromPosition(Vector3 position, out int i, out int j)
    {
        float centerMatixX = (_widthCard * (row + 1)) / 2;
        float centerMatixY = (_heightCard * (column + 1)) / 2;

        position.x += centerMatixX;
        position.y += centerMatixY;

        position.x /= _widthCard;
        position.y /= _heightCard;

        i = Mathf.RoundToInt(position.x - 1);
        j = Mathf.RoundToInt(position.y - 1);
    }


    internal string GetData()
    {
        int[] intDate = new int[countCard];
        int i = 0;
        foreach (var item in cards)
        {
            intDate[i] = (item) ? item.type : -1;
            i++;
        }

        return JsonConvert.SerializeObject(intDate);
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool gameStart { get; private set; }

    public CardMatrix cardMatrix;
    public int row, column;
    public float flipAllCardOnStartAfter = 1.4f;

    int _matchScore, _turnScore;
    bool _gameSaved;

    public System.Action<int, int> OnMatchScoreChnage;

    public int MatchScore => _matchScore;
    public int TurnScore => _turnScore;

    private void Awake()
    {
        instance = this;
    }

    public void GameStart(int row, int column)
    {
        if (!gameStart)
        {
            this.row = row;
            this.column = column;
            GameStart();
        }
    }

    public void GameStart()
    {
        if (!gameStart)
        {
            gameStart = true;
            _gameSaved = true;
            _matchScore = 0;
            _turnScore = 0;
            OnMatchScoreChnage?.Invoke(_matchScore, _turnScore);

            PlayerPrefs.DeleteAll();
        }
    }
    public void GameStartPrevProgress()
    {
        if (!gameStart)
        {
            gameStart = true;
            _gameSaved = true;
            OnMatchScoreChnage?.Invoke(_matchScore, _turnScore);
        }
    }

    public void GameEnd()
    {
        if (gameStart)
        {
            gameStart = false;
        }
    }

    public void ResetGame()
    {
        gameStart = false;
        _gameSaved = true;
        _matchScore = 0;
        _turnScore = 0;
    }

    public void AddMatchScore()
    {
        _gameSaved = false;
        _matchScore++;
        _turnScore++;
        OnMatchScoreChnage?.Invoke(_matchScore, _turnScore);
    }
    public void AddTurnScore()
    {
        _gameSaved = false;
        _turnScore++;
        OnMatchScoreChnage?.Invoke(_matchScore, _turnScore);
    }
}

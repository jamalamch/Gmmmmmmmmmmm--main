using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool gameStart { get; private set; }


    public CardMatrix cardMatrix;
    public UiManager uiManager;
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
        uiManager.Init();
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
            uiManager.DoSpalsh(() =>
            {
                uiManager.OpenLeveUIl();
                cardMatrix.Init(row, column);
            });
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
            row = PlayerPrefs.GetInt("rowMatrix", 2);
            column = PlayerPrefs.GetInt("columnMatrix", 2);
            string data = PlayerPrefs.GetString("dataMatrix", "");

            uiManager.DoSpalsh(() =>
            {
                uiManager.OpenLeveUIl();
                cardMatrix.Init(row, column, data);
            });
            _matchScore = PlayerPrefs.GetInt("matchScore", 0);
            _turnScore = PlayerPrefs.GetInt("turnScore", 0);
            OnMatchScoreChnage?.Invoke(_matchScore, _turnScore);

            PlayerPrefs.DeleteAll();
        }
    }

    public void GameEnd()
    {
        if (gameStart)
        {
            gameStart = false;
            uiManager.OpenEndLevelUIl();
        }
    }

    public void ResetGame()
    {
        gameStart = false;
        _gameSaved = true;
        _matchScore = 0;
        _turnScore = 0;
        uiManager.DoSpalsh(() =>
        {
            cardMatrix.ResetMatrix();
            uiManager.OpenStartMenuUI();
        });
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

    void SaveGameProgress()
    {
        if (gameStart && !_gameSaved)
        {
            Debug.Log("Save progress");
            _gameSaved = true;
            PlayerPrefs.SetInt("matchScore", _matchScore);
            PlayerPrefs.SetInt("turnScore", _turnScore);
            PlayerPrefs.SetInt("rowMatrix", cardMatrix.row);
            PlayerPrefs.SetInt("columnMatrix", cardMatrix.column);
            PlayerPrefs.SetString("dataMatrix", cardMatrix.GetData());
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveGameProgress();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameProgress();
    }
}

using UnityEngine;

public class LevelPanelUI : PanelUI
{
    [SerializeField] TMPro.TMP_Text _scoreTxt;
    [SerializeField] TMPro.TMP_Text _turnTxt;
    [SerializeField] ButtonUI _exitButton;

    public override void Init()
    {
        base.Init();
        _scoreTxt.text = "0";
        _turnTxt.text = "0";

        GameManager.instance.OnMatchScoreChnage += (score, turn) =>
        {
            _scoreTxt.text = score.ToString();
            _turnTxt.text = turn.ToString();
        };

        _exitButton.Init(GameManager.instance.ResetGame);
    }
}

using UnityEngine;

public class EndPanelUI : PanelUI
{
    [SerializeField] TMPro.TMP_Text _scoreTxt;
    [SerializeField] TMPro.TMP_Text _turnTxt;
    [SerializeField] ButtonUI _nextButton;

    public override void Init()
    {
        base.Init();
        _nextButton.Init(GameManager.instance.ResetGame);
    }

    public override void Open()
    {
        _scoreTxt.text = GameManager.instance.MatchScore.ToString();
        _turnTxt.text = GameManager.instance.TurnScore.ToString();
        base.Open();
    }
}

using UnityEngine;

public class StartPanelUI : PanelUI
{
    [Header("Start Menu")]
    [SerializeField] ButtonUI startButton2x2;
    [SerializeField] ButtonUI startButton2x3;
    [SerializeField] ButtonUI startButton4x5;
    [SerializeField] ButtonUI startButton5x6;

    [SerializeField] ButtonUI continueButton;

    public override void Init()
    {
        base.Init();
        startButton2x2.Init(() => GameManager.instance.GameStart(2, 2));
        startButton2x3.Init(() => GameManager.instance.GameStart(2, 3));
        startButton4x5.Init(() => GameManager.instance.GameStart(4, 5));
        startButton5x6.Init(() => GameManager.instance.GameStart(5, 6));

        if (PlayerPrefs.HasKey("dataMatrix"))
        {
            continueButton.Init(() =>
            {
                GameManager.instance.GameStartPrevProgress();
                Destroy(continueButton.gameObject, 1);
            });
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }
}

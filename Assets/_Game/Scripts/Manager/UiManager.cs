using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Image _splashImage;
    PanelUI[] _allPanel;

    public void Init()
    {
        _allPanel = GetComponentsInChildren<PanelUI>(true);
        foreach (var item in _allPanel)
        {
            item.Init();
        }
        _splashImage.DOFade(0, 0);
        OpenStartMenuUI();
    }

    public void OpenStartMenuUI()
    {
        Open<StartPanelUI>();
    }

    public void OpenLeveUIl()
    {
        Open<LevelPanelUI>();
    }

    public void OpenEndLevelUIl()
    {
        Open<EndPanelUI>();
    }


    public void Open<T>() where T : PanelUI
    {
        foreach (var item in _allPanel)
        {
            if (item is T)
                item.Open();
            else
                item.Close();
        }
    }

    public void OpenWithouCloseOther<T>() where T : PanelUI
    {
        foreach (var item in _allPanel)
        {
            if (item is T)
                item.Open();
        }
    }

    public T GetPage<T>() where T : PanelUI
    {
        foreach (var item in _allPanel)
        {
            if (item is T)
                return (T)item;
        }

        return null;
    }

    public void DoSpalsh(System.Action onSplash)
    {
        _splashImage.DOFade(1, 0.3f).OnComplete(() =>
        {
            onSplash?.Invoke();
            _splashImage.DOFade(0, 0.2f).SetDelay(0.12f);
        });
    }
}

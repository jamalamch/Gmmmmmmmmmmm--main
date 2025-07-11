using System;
using UnityEngine;
using UnityEngine.UI;



public class ButtonUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private Button _button;

    public RectTransform Rect => _rect;
    public Button Button => _button;

    public void ReInit(Action callback)
    {
        _button.onClick.RemoveAllListeners();
        Init(callback);
    }

    public void Init(Action callback)
    {
        _button.onClick.AddListener(() =>
        {
            callback();
            //AudioManager.Play("Click");
        });
    }

    public void Interactable(bool interactable)
    {
        _button.interactable = interactable;
    }

    private void OnValidate()
    {
        if (!_button)
            _button = GetComponentInChildren<Button>();
        if (!_rect)
            _rect = (RectTransform)transform;
    }
}

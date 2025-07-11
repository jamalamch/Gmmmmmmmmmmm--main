using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    protected bool _open;
    protected bool _onUpdate;

    public RectTransform Rect => _rect;
    public bool isOpen => _open;

    protected float _BagroundImageFade;

    public void Open(Vector2 AtPosition)
    {
        _openPanel = AtPosition;
        Open();
    }

    [ContextMenu("Open")]
    public virtual void Open()
    {
        if (_onUpdate)
            return;
        print("Open Panel " + name);
        if (!_open)
        {
            _rect.gameObject.SetActive(true);
            _open = true;
            _onUpdate = true;
        }
    }

    [ContextMenu("Close")]
    public virtual void Close()
    {
        if (_onUpdate)
            return;
        print("Close Panel " + name);
        if (_open)
        {
             _rect.gameObject.SetActive(false);
            _open = false;
            _onUpdate = true;
        }
    }

    void EndClose()
    {
        _rect.gameObject.SetActive(false);
        _onUpdate = false;
    }

    void EndOpen()
    {
        _onUpdate = false;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Camera _camreaRay;

    private bool _click;
    public bool Click => _click;

    public Vector2 pointer { get; private set; }
    public Vector3 pointerWord { get; private set; }


    public void OnPointerClick(PointerEventData eventData)
    {
        _click = true;
        pointer = eventData.position;
        pointerWord = _camreaRay.ScreenToWorldPoint(pointer);
    }

    void LateUpdate()
    {
        _click = false;
    }
}

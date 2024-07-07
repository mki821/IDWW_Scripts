using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform _dragRectTransform;

    public void OnDrag(PointerEventData eventData) {
        _dragRectTransform.anchoredPosition += eventData.delta;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class CardScaleTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 _defaultScale;

    private void OnEnable()
    {
        _defaultScale = transform.localScale;
    }

    private void OnDisable()
    {
        transform.localScale = _defaultScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _defaultScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(_defaultScale.x * 2.5f, _defaultScale.y, _defaultScale.z * 2.5f);
    }
}

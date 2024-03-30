using UnityEngine;
using UnityEngine.EventSystems;

public class CardScaleTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 _defaultScale;
    Vector3 _defaultPos;

    private void OnEnable()
    {
        _defaultScale = transform.localScale;
        _defaultPos = transform.position;
    }

    private void OnDisable()
    {
        transform.localScale = _defaultScale;
        transform.localPosition = _defaultPos;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _defaultScale;
        transform.position = _defaultPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(_defaultScale.x * 2.5f, _defaultScale.y, _defaultScale.z * 2.5f);
        transform.position = new Vector3(_defaultPos.x, 15f, _defaultPos.z);
    }
}

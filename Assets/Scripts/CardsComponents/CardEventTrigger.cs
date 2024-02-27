using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardEventTrigger : MonoBehaviour, IDragHandler , IEndDragHandler
{
    CardAnimationController _cardAnim;
  
    private void OnEnable()
    {
       _cardAnim = GetComponent<CardAnimationController>(); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.transform.position.y;         
        Vector3 newPos = Camera.main.ScreenToWorldPoint(screenPos);       
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 currectPos = transform.position;
        RaycastHit hit;

        if(Physics.Raycast(currectPos,Vector3.down,out hit)) 
        {
            if(hit.transform.CompareTag("Player1"))
            {
                  transform.SetParent(hit.transform);
                    transform.localPosition = Vector3.zero;
                    _cardAnim.OnHideOrShowCard(false);                            
            }           
        }

        transform.localPosition = Vector3.zero;
    }  
}

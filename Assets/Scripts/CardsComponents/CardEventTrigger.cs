using UnityEngine;
using UnityEngine.EventSystems;


public class CardEventTrigger : MonoBehaviour, IDragHandler , IEndDragHandler
{   
    CardAnimationController _cardAnim;
    Player _currectPlayer;
    CardCost _cost;

    Vector3 _defaultScale;

    
    private void OnEnable()
    {       
       _cost = GetComponent<CardCost>();
       _cardAnim = GetComponent<CardAnimationController>();
        _currectPlayer = GetComponent<CardPlayerService>().CurrectPlayer();      
    }

    public Player GetPlayerComponent() => _currectPlayer;

    public void OnDrag(PointerEventData eventData)
    {
        _currectPlayer.ShowCardOrHidePlaces(true);
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.transform.position.y;         
        Vector3 newPos = Camera.main.ScreenToWorldPoint(screenPos);       
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);        
        GetComponent<CardScaleTrigger>().enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 currectPos = transform.position;
        RaycastHit hit;

        if(Physics.Raycast(currectPos,Vector3.down,out hit)) 
        {
            if(hit.transform.CompareTag(gameObject.tag))
            {
                if (_cost.SpendPoints())
                {
                    transform.SetParent(hit.transform, true);
                    _cardAnim.PutCardAnimation(1);
                    gameObject.AddComponent<CardFightEventTrigger>();
                    GetComponent<CardScaleTrigger>().enabled = true;
                    _currectPlayer.AddCardOnTable(gameObject);
                    Destroy(this);
                }
            }           
        }

        if (transform.parent != null)
        {
            transform.localPosition = Vector3.zero;
        }

        _currectPlayer.ShowCardOrHidePlaces(false);
    }
 
}
    
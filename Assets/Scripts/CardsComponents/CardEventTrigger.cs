using UnityEngine;
using UnityEngine.EventSystems;


public class CardEventTrigger : MonoBehaviour, IDragHandler , IEndDragHandler
{   
    CardAnimationController _cardAnim;

    CardPlayerService _player;
    Player _currectPlayer;

    CardCost _cost;

    private void OnEnable()
    {       
       _cost = GetComponent<CardCost>();
       _cardAnim = GetComponent<CardAnimationController>();
       _player = GetComponent<CardPlayerService>();
       _currectPlayer = _player.CurrectPlayer();

       // if (_currectPlayer != null)
       // { 
       //    Debug.Log($"{_currectPlayer} - у меня инъекция этого игрока"); 
       // }

    }

    public Player GetPlayerComponent() => _currectPlayer;

    public void OnDrag(PointerEventData eventData)
    {
        _currectPlayer.ShowCardOrHidePlaces(true);
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
            if(hit.transform.CompareTag(gameObject.tag))
            {
                if (_cost.SpendPoints())
                {
                    transform.SetParent(hit.transform);
                    _cardAnim.PutCardAnimation(1);
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
    
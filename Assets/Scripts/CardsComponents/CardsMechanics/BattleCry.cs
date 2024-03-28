using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCry : MonoBehaviour, IPointerClickHandler
{
    CardPropities _cardPropities;

    [SerializeField] Transform _startObject;
    [SerializeField] GameObject _endObject;

    bool _battleCryActive = false;
    [SerializeField]int _attackDamage;

    private void OnEnable()
    {
        _cardPropities = GetComponent<CardPropities>();
        ReadDescreptionCard();
    }

    public void ReadDescreptionCard()
    {
        string descreption = _cardPropities._cardDescription;
        string pattern = @"\s+(\d+)\s+";
        
        Match match = Regex.Match(descreption, pattern);
        if (match.Success)
        {
            string numberString = match.Groups[1].Value;
            _attackDamage = int.Parse(numberString);
            Debug.Log($"{_attackDamage} - бафф карты на атаку");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _startObject = transform;
        if(_startObject != null) 
        {
           _battleCryActive = true;
        }       
    }

    void Update()
    {
        if (_battleCryActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (_endObject == null)
                    {
                        _endObject = hit.transform.gameObject;
                        Debug.Log("Выбрана конечная точка: " + _startObject.name);
                        ActiveBattleCry(_endObject);
                    }
                }
            }
        }
    }

    private void ActiveBattleCry(GameObject enemyCard)
    {
        if(enemyCard.GetComponent<Player>())
        {
            enemyCard.GetComponent<Player>().Health -= _attackDamage;
            enemyCard.GetComponent<Player>().UpdateHealthInfo();
        }   
        else if(enemyCard.GetComponent<CardPropities>())
        {
            enemyCard.GetComponent<CardPropities>()._cardHealth -= _attackDamage;
            enemyCard.GetComponent<CardPropities>().UpdateHealthAfterAttack();
        }
        
        Destroy(this);
    }
      
      
}

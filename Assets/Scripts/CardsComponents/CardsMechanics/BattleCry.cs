using System;
using System.Text.RegularExpressions;
using Cards;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCry : MonoBehaviour, IPointerClickHandler
{  
    [SerializeField] int _attackDamage;

    [SerializeField] int _restoreHealthValue;
    [SerializeField] int _damageBuffValue;

    [SerializeField] Transform _startObject;
    [SerializeField] GameObject _endObject;

    [SerializeField] bool _battleCryActive = false;
    [SerializeField] bool _thisNeed = true;

    [SerializeField]string _descreption;
    [SerializeField]ActionType _actionType;

    CardPropities _cardPropities;
    Player _currectPlayer;

    private void OnEnable()
    {
        _cardPropities = GetComponent<CardPropities>();
        _currectPlayer = GetComponent<CardPlayerService>().CurrectPlayer();
        ReadDescreptionCard();
    }
    
    public void ReadDescreptionCard()
    {      
        _descreption = _cardPropities._cardDescription;       
        string typeBattleCry = @"Battlecry: (\w+)";

        Match match = Regex.Match(_descreption, typeBattleCry);
        if(match.Success)
        {
            string type = match.Groups[1].Value;
            Enum.TryParse(type, out _actionType);
            BuffParamets();
            if(_actionType == ActionType.Gain)
            {
                _thisNeed = false;
                BattleCryType(_actionType);
            }
        }
    }

    private void BuffParamets()
    {
        string pattern = @"\b\d+\b";
        Regex numberRegex = new Regex(pattern);
        MatchCollection matches = numberRegex.Matches(_descreption);

        if (matches.Count == 2)
        {
            _restoreHealthValue = int.Parse(matches[0].Value);
            _damageBuffValue = int.Parse(matches[1].Value);
        }

        if (matches.Count == 1)
        {
            Regex wordRegex = new Regex(@"\b(Health|damage)\b", RegexOptions.IgnoreCase);
            Match match = wordRegex.Match(_descreption);
            if (match.Success)
            {
                string matchFound = match.Groups[1].Value;
                if (matchFound == "Health")
                {
                    _restoreHealthValue = int.Parse(matches[0].Value);
                }
                else if (matchFound == "damage")
                {
                    if (_actionType == ActionType.Deal)
                    {
                        _attackDamage = int.Parse(matches[0].Value);
                    }
                    else
                    {
                        _damageBuffValue = int.Parse(matches[0].Value);
                    }
                }
            }
        }
    }

    private void BattleCryType(ActionType type)
    {
        switch(type)
        {
            case ActionType.Deal:
                OnDeal();
                break;
            case ActionType.Restore:
                OnRestore();
                break;               
            case ActionType.Give:
                OnGive();
                break;                
            case ActionType.Gain:
                OnGain();
                break;
        }            
    }

    private void OnDeal() 
    {
        var player = _endObject.GetComponent<Player>();
        var card = _endObject.GetComponent<CardPropities>();

        Regex wordRegex = new Regex(@"\b(enemy|damage)\b", RegexOptions.IgnoreCase);
        Match match = wordRegex.Match(_descreption);
        if (match.Success)
        {
           string checkEnemy = match.Groups[1].Value;
           if (checkEnemy == "enemy")
           {
                player.Health -= _attackDamage;
                player.UpdateHealthInfo();
            }
           else
           {
               if(_endObject.GetComponent<Player>())
               {
                    player.Health -= _attackDamage;
                    player.UpdateHealthInfo();
               }
               else if(_endObject.GetComponent<CardPropities>())
               {
                    card._cardHealth -= _attackDamage;
                    card.UpdateHealthAfterAttack();
               }
           }

            DestroyBattleCry();
        }
    }
    
    private void OnGive() 
    {
        var card = _endObject.GetComponent<CardPropities>();

        if(_endObject.CompareTag(gameObject.tag))
        {
            card._cardHealth += _restoreHealthValue;
            card._cardDamage += _damageBuffValue;
        }
        
        card.UpdateDataHealthAndAttack();
        DestroyBattleCry();
    }
    
    private void OnGain() 
    {
       if(_currectPlayer != null)
       {
            foreach(var card in _currectPlayer.CardsOnTable)
            {
                if(card == this.gameObject) { continue; }

                _cardPropities._cardHealth += _restoreHealthValue;
                _cardPropities._cardDamage += _damageBuffValue;
            }

            _cardPropities.UpdateDataHealthAndAttack();
            DestroyBattleCry();
       }
    }
    
    private void OnRestore() 
    {
       if(_endObject.CompareTag(gameObject.tag))
       {
           var player = _endObject.GetComponent<Player>();
           var card = _endObject.GetComponent<CardPropities>();

           if (_endObject.GetComponent<Player>())
           {
                player.Health += _restoreHealthValue;
                player.UpdateHealthInfo();
            }
           else if(_endObject.GetComponent<CardPropities>())
           {
                card._cardHealth += _restoreHealthValue;
                card.UpdateDataHealthAndAttack();
           }

            DestroyBattleCry();
       }
    }
 
    public void OnPointerClick(PointerEventData eventData)
    {    
        if(_thisNeed)
        {
            _startObject = transform;
            
            if (_startObject != null)
            {
                _battleCryActive = true;
            }
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
                        BattleCryType(_actionType);
                        Debug.Log("Выбрана конечная точка: " + _endObject.name);
                    }
                }
            }
        }
    }

    private void DestroyBattleCry()
    {
        Destroy(this);
    }
            
}
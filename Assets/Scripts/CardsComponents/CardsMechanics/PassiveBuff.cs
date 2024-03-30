using Cards;
using System.Text.RegularExpressions;
using UnityEngine;

public class PassiveBuff : MonoBehaviour
{
    Player _currectPlayer;
    CardPropities _card;

    [SerializeField]PassiveBuffType _buffType;
    [SerializeField]string description;

    [SerializeField] int _cardhealth;
    [SerializeField] bool _selfBuff;

    [SerializeField] int _buffHealth;
    [SerializeField] int _buffDamage;

    private void OnEnable()
    {
        _currectPlayer = GetComponent<CardPlayerService>().CurrectPlayer();    
        _card = GetComponent<CardPropities>();
        ReadDescreptionCard();
    }

    private void Update()
    {
        if(_card._cardHealth != _cardhealth && _selfBuff == true)
        {
            BuffDamage();
            _cardhealth = _card._cardHealth;
        }
    }

    private void ReadDescreptionCard()
    {
        description = _card._cardDescription;

        string quantityPattern = @"(?:other |this )";
        string typePattern = @"(?:other |this )?(minions|Murlocs|minion)";       
        string numberPattern = @"\+\d(?:/\+\d|\sAttack)?";

        Match quantityMatch = Regex.Match(description, quantityPattern);
        Match numberMatch = Regex.Match(description, numberPattern);
        Match typeMatch = Regex.Match(description, typePattern);
        
        string quantity = quantityMatch.Value.Trim();
        string type = typeMatch.Groups[1].Value.Trim();
        string number = numberMatch.Value.Trim();

        Debug.Log($"{quantityMatch} - кол-во");
        Debug.Log($"{type} - тип");
        Debug.Log($"{number} - на сколько увеличить урон");

        BuffParamets(number);

        if (quantity == "this")
        {
            _buffType = PassiveBuffType.Self;
        }
        else if (quantity == "other")
        {
            _buffType = PassiveBuffType.Anybody;
        }

        SelectBuffType(_buffType, type);
    }
  
    private void SelectBuffType(PassiveBuffType _type, string _cardType)
    {
        switch(_type)
        {
            case PassiveBuffType.Self:
                OnSelfBuff();
                break;
                
            case PassiveBuffType.Anybody:
                OnBuffAnybody(_cardType);
                break;                                      
        }
    }

    private void OnSelfBuff()
    {
        BuffDamage();
    }

    private void OnBuffAnybody(string _cardType)
    {
        if(_cardType == "Murlocs")
        {
            OnBuffCards();
        }
        else
        {
            OnOtherBuffCards(_buffDamage, _buffHealth);
        }
    }

    private void BuffDamage()
    {     
        _card._cardDamage += _buffDamage;
        _card.UpdateDataHealthAndAttack();

        if (_selfBuff != true)
        {
            _cardhealth = _card._cardHealth;
            _selfBuff = true;
        }

    }

    private void BuffParamets(string numbers)
    {
        string pattern = @"\b\d+\b";
        Regex numberRegex = new Regex(pattern);
        MatchCollection matches = numberRegex.Matches(numbers);

        if(matches.Count == 2)
        {
            _buffHealth = int.Parse(matches[0].Value);
            _buffDamage = int.Parse(matches[1].Value);
        }
        else if (matches.Count == 1)
        {
            _buffDamage = int.Parse(matches[0].Value);
        }
    }

    public void OnBuffCards()
    {
        foreach(var card in _currectPlayer.CardsOnTable)
        {            
            var unionCard = card.GetComponent<CardPropities>();

            if(unionCard.gameObject == this.gameObject)
            {
                continue;
            }
           
            if (_card._cardType == unionCard._cardType)
            {
                unionCard._cardDamage += _buffDamage;
                unionCard.UpdateDataHealthAndAttack();
            }
        }
    }

    public void OnOtherBuffCards(int _damage, int _health)
    {
        foreach(var card in _currectPlayer.CardsOnTable)
        {
            var unionCard = card.GetComponent<CardPropities>();

            if (this.gameObject == unionCard.gameObject) { continue; }

            unionCard._cardDamage += _damage;
            unionCard._cardHealth += _health;
            unionCard.UpdateDataHealthAndAttack();
        }
    }
}

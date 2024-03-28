using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PassiveBuff : MonoBehaviour
{
    Player _currectPlayer;
    CardPropities _card;

    private void OnEnable()
    {
        _currectPlayer = GetComponent<CardPlayerService>().CurrectPlayer();    
        _card = GetComponent<CardPropities>();
        ReadDescreptionCard();
    }

    public void ReadDescreptionCard()
    {
        string descreption = _card._cardDescription;
        string pattern = @"\+(\d+)";
        int buffDamage = 0;

        Match match = Regex.Match(descreption, pattern);
        if (match.Success)
        {
            string numberString = match.Groups[1].Value;
            buffDamage = int.Parse(numberString);
            Debug.Log($"{buffDamage} - бафф карты на атаку");
        }

        OnBuffCard(buffDamage);
    }

    public void OnBuffCard(int BuffAttack)
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
                unionCard._cardDamage += BuffAttack;
                unionCard.UpdateDataHealthAndAttack();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : MonoBehaviour
{
    Player _currectPlayer;
    CardPropities _cards;

    private void OnEnable()
    {
        _currectPlayer = GetComponent<CardPlayerService>().CurrectPlayer();
        _cards = GetComponent<CardPropities>();
        CheckTaunt();
    }

    private void CheckTaunt() 
    {
       foreach(var card in _currectPlayer.CardsOnTable)
       {
            if(card == this.gameObject || card.GetComponent<Taunt>())
            {
                continue;
            }
            else
            {
                var obj = card.GetComponent<CardFightEventTrigger>();
                if(obj._AttackResistance == false)
                {
                    obj._AttackResistance = true;
                }
            }
       }

       if(_currectPlayer._AttackResistance != false)
       {
           _currectPlayer._AttackResistance = true;
       }
    }

    private void OnDestroy()
    {
        foreach(var card in _currectPlayer.CardsOnTable)
        {
            if (card == this.gameObject)
            {
                continue;
            }

            if (card.GetComponent<Taunt>())
            {
                return;
            }

            if(card.GetComponent<CardFightEventTrigger>()._AttackResistance == true)
            {
                card.GetComponent<CardFightEventTrigger>()._AttackResistance = false;
            }
        }

        _currectPlayer._AttackResistance = false;
    }
}

using Cards;
using Cards.ScriptableObjects;
using OneLine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class DeckGeneration : MonoBehaviour
{
    [Header("Основные настройки:")]
    [SerializeField,Range(1,30)] private int _cardIssuance;   
   
    [Header("Дебаг листа на наличие данных:")]
    [SerializeField,OneLine(Header = LineHeader.Short)]
    private List<CardPropertiesData> _cardsData ;
    
    [Inject(Id = "Player1")]private Player _playerFirst;
    [Inject(Id = "Player2")]private Player _playerSecond;
    [Inject]private CardPlayerService.Factory _cardFactory;
   

    private void Awake()
    {
        OnCardIssuance();
    }

    private void OnCardIssuance()
    {
        CardsGeneration(_playerFirst.Transform, _playerFirst, _playerSecond, _playerFirst._cardPack._cards);
        CardsGeneration(_playerSecond.Transform, _playerSecond, _playerFirst, _playerSecond._cardPack._cards);
    }

    private void CardsGeneration(Transform cardPosition, Player _currectPlayer, Player _enemyPlayer, List<GameObject> card)
    {
        for(int i = 0; i< _cardIssuance ; i++) 
        {
            CardPlayerService newCard = _cardFactory.Create(_currectPlayer, _enemyPlayer, card[i]);
            var obj = newCard.gameObject;
            obj.tag = _currectPlayer.gameObject.tag;
            obj.transform.SetParent(cardPosition);
            obj.transform.SetPositionAndRotation(cardPosition.transform.position + new Vector3(0,i,0), cardPosition.transform.rotation);
            _currectPlayer.AddCard(newCard.gameObject);
        }
    }
}

using Cards;
using Cards.ScriptableObjects;
using OneLine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        _cardsData = _playerFirst.CardPack.UnionProperties(_cardsData).ToList();
        CardsGeneration(_playerFirst.Transform, _playerFirst);
        _cardsData.Clear();
        _cardsData = _playerSecond.CardPack.UnionProperties(_cardsData).ToList();
        CardsGeneration(_playerSecond.Transform, _playerSecond);
    }

    private void CardsGeneration(Transform cardPosition, Player _currectPlayer)
    {
        for(int i = 0; i< _cardIssuance ; i++) 
        {
            CardPlayerService newCard = _cardFactory.Create(_currectPlayer);
            var obj = newCard.gameObject;
            obj.tag = _currectPlayer.gameObject.tag;
            obj.transform.SetParent(cardPosition);
            obj.transform.SetPositionAndRotation(cardPosition.transform.position + new Vector3(0,i,0), cardPosition.transform.rotation);
            var propities = obj.GetComponent<CardPropities>();
            propities.OnUpdateCardData(_cardsData[i]);
            _currectPlayer.AddCard(newCard.gameObject);
        }
    }
}

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
    [Header("ќсновные настройки:")]
    [SerializeField,Range(1,30)] private int _cardIssuance;   
    [SerializeField] private GameObject _card;

    [Header("—сыллки 1-го игрока:")]
    [SerializeField] private CardPackConfiguration _firstPlayerConfigCardData;
    [SerializeField] private Transform _firstPlayerTransform;

    [Header("—сыллки 2-го игрока:")]
    [SerializeField] private CardPackConfiguration _secondPlayerConfigCardData;
    [SerializeField] private Transform _secondPlayerTransform;

    [Header("ƒебаг листа на наличие данных:")]
    [SerializeField, OneLine(Header = LineHeader.Short)]
    private List<CardPropertiesData> _cardsData ;
    
    [Inject] private Player _player;
    [Inject] private DeckGenerationInstaller _installer;

    private void Awake()
    {
        OnCardIssuance();
    }

    private void OnCardIssuance()
    {
        _cardsData = _firstPlayerConfigCardData.UnionProperties(_cardsData).ToList();
        CardsGeneration(_firstPlayerTransform);
        _cardsData.Clear();
        _cardsData = _secondPlayerConfigCardData.UnionProperties(_cardsData).ToList();
        _installer.PlayerReBind();
        _player = _installer.GetCurrentPlayer();
        CardsGeneration(_secondPlayerTransform);
    }

    private void CardsGeneration(Transform cardPosition)
    {
        for (int i = 0; i < _cardIssuance; i++)
        {
            var cardObj = Instantiate(_card, cardPosition.position, Quaternion.identity);
            cardObj.transform.SetParent(cardPosition.transform, true);
            var obj = cardObj.GetComponent<CardPropities>();
            obj.OnUpdateCardData(_cardsData[i]);
            _player.AddCard(cardObj);
        }
    }
}

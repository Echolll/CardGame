using Cards.ScriptableObjects;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Header("Обязательные настройки:")]
    [SerializeField] private CardPackConfiguration _cardPack;
    [SerializeField] private Transform _transform;

    [Header("Основные настройки:")]
    [SerializeField,Range(0,30)] private int _maxHealth;
    [SerializeField,Range(1,10)] private int _maxMagicPoints;
    [SerializeField,Range(0,10)] private int _magicPoints;

    [Header("Другое:")]
    [SerializeField] private List<GameObject> _cardsInStock;
    [SerializeField] private List<GameObject> _cardsInHand;

    [SerializeField] private List<GameObject> _cardPlace;
    [SerializeField] private CardPlaceInHand[] _cardPlaceInHand;

    [Inject] private SelectStarterCards _startCards;

    ChangeColor _color;
    private bool _isPlayed = true;

    private void Start()
    {
        if(_startCards == null) { Debug.LogWarning($"Есть сыллка на объект {_startCards}");}
        _color = GetComponent<ChangeColor>();
    }

    public CardPackConfiguration CardPack
    {
        get { return _cardPack; }
    }

    public Transform Transform 
    { 
        get 
        { return _transform; } 
    }

    public int MagicPoint
    {
        get { return _magicPoints; }
        set { _magicPoints = value; }
    }

    public int MaxMagicPoint
    {
        get { return _maxMagicPoints;}
    }
   
    public int GetMagicPoints() => _magicPoints;

    public int GetCardsInHand() => _cardsInHand.Count;

    public void AddCard(GameObject card) => _cardsInStock.Add(card);
    
    public void AddCardToHand(GameObject card) => _cardsInHand.Add(card);

    public void AddCardToHandAfterMove()
    {
        if (_cardsInStock.Count != 0)
        {
            var card = _cardsInStock[0].gameObject;
            _cardsInHand.Add(card);
            RemoveCard(card);
        }
        else { Debug.Log("Карты кончились! + 1 урон"); }
    }

    public void RemoveCard(GameObject card)
    {
        if(_cardsInStock.Contains(card))
        {
            _cardsInStock.Remove(card);
        }
    }

    public void RemoveCardInHand(GameObject card)
    {
        if (_cardsInHand.Contains(card))
        {
            _cardsInHand.Remove(card);
        }
    }
    
    public void SelectCardOnStart()
    {
        if(_isPlayed)
        {
            _startCards.OnShowCard(_cardsInStock);            
            _isPlayed = false; 
        }
    }

    public void OnPutCardToHand()
    {
        foreach(var card in _cardsInHand)
        {
            for(int i = 0; i < _cardPlaceInHand.Length; i++) 
            {
                if (_cardPlaceInHand[i].ThisPlaceAvalible())
                {
                    card.gameObject.transform.parent = null;
                    card.gameObject.transform.position = _cardPlaceInHand[i].gameObject.transform.position;
                    //card.gameObject.transform.rotation = _cardPlaceInHand[i].gameObject.transform.rotation;
                    card.gameObject.transform.parent = _cardPlaceInHand[i].gameObject.transform;
                }
            }
        }
    }

    public bool CheckCardsInHand()
    {
        if(_cardsInHand.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }
    
    public void ShowCardOrHidePlaces(bool useCard)
    {
        if(useCard)
        {
            _color.ChangePlaceMaterial(_cardPlace);
        }
        else
        {
            _color.DeleteMaterial(_cardPlace);
        }
    }

    public void PutCardsToStock()
    {
        for(int i = 0; _cardsInStock.Count > i; i++)
        {         
            _cardsInStock[i].gameObject.transform.parent = null;       
            _cardsInStock[i].gameObject.transform.localPosition = _transform.localPosition + new Vector3(0,i,0);
            _cardsInStock[i].gameObject.transform.rotation = _transform.rotation;
            _cardsInStock[i].gameObject.transform.SetParent(_transform);
        }
        
    }

    public void ShowOrHideCard(bool _showCard)
    {
        int animName = _showCard == true ? 2 : 3;

        foreach (var card in _cardsInHand)
        {
            card.GetComponent<CardAnimationController>().PutCardAnimation(animName);
        }

    }

    public void RestoreManaPoint()
    {
        if (_maxMagicPoints != 10)
        {
            _maxMagicPoints += 1;
        }

        _magicPoints = _maxMagicPoints;
    }
}

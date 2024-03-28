using Cards;
using Cards.ScriptableObjects;
using OneLine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SpawnCards : MonoBehaviour
{
    [SerializeField]Transform _parent;
    [SerializeField]GameObject _cardPrefab;

    [SerializeField]List<CardPackConfiguration> _cardPacks;

    [SerializeField, OneLine(Header = LineHeader.Short)]
    private List<CardPropertiesData> _cardsData;
    
    [SerializeField]
    private List<GameObject> _cards;

    [ContextMenu("Spawning Cards")]
    private void OnSpawnCards()
    {
        AddCardData();
        GenerationCards();
    }

    private void AddCardData()
    {
        for(int i = 0; i < _cardPacks.Count; i++) 
        {
            _cardsData = _cardPacks[i].UnionProperties(_cardsData).ToList();
        }       
    }
    
    private void GenerationCards()
    {
        foreach(var cardData in  _cardsData)
        {           
            var obj = Instantiate(_cardPrefab, new Vector3(0,0,0), Quaternion.identity);
            obj.transform.SetParent(_parent);
            obj.GetComponent<CardPropities>().OnUpdateCardData(cardData);
            _cards.Add(obj);
        }
    }
    
    
}

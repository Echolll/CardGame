using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SelectStarterCards : MonoBehaviour
{
    [SerializeField] private List<Transform> _cardPlaces;
    [SerializeField] private List<GameObject> _cards;
    
    [Inject] UIPanelAnimationController _panelAnimator;

    public void OnShowCard(List<GameObject> _playerCards)
    {
        List<int> _randNumb = new List<int>();
      
        GetRandomNumber(_randNumb, _playerCards.Count, _cardPlaces.Count);

        for (int i = 0; i < _cardPlaces.Capacity; i++)
        {
            _cards.Add(_playerCards[_randNumb[i]]);
            _playerCards[_randNumb[i]].transform.SetParent(_cardPlaces[i]);
            _playerCards[_randNumb[i]].transform.position = _cardPlaces[i].transform.position;
            _playerCards[_randNumb[i]].transform.rotation = _cardPlaces[i].transform.rotation;
            _playerCards[_randNumb[i]].gameObject.AddComponent<StartedEventTrigger>();
            _playerCards[_randNumb[i]].GetComponent<StartedEventTrigger>().UIPanelAnimationController(_panelAnimator);
            _playerCards[_randNumb[i]].GetComponent<StartedEventTrigger>().SelectStarterCards(this);
        }

        _panelAnimator.ChooseCardOnStart();
    }

    private void GetRandomNumber(List<int> _randNumList, int _cardsCount, int _cardsPlaceCount)
    {
        while (_randNumList.Count < _cardPlaces.Count)
        {
            int randNum = Random.Range(0, _cardsPlaceCount);

            if (!_randNumList.Contains(randNum))
            {
                _randNumList.Add(randNum);
            }
        }       
    }
   
}
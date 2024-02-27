using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class SelectStarterCards
{
    public static void OnShowCard(List<GameObject> _playerCards, List<Transform> _cardPlaces)
    {
        List<int> userNumb = new List<int>();

        GetRandomNumber(_playerCards.Capacity ,userNumb, _cardPlaces.Capacity);

        for (int i = 0; i < _cardPlaces.Capacity; i++)
         {
            _playerCards[userNumb[i]].transform.position = _cardPlaces[i].transform.position;
            _playerCards[userNumb[i]].transform.rotation = _cardPlaces[i].transform.rotation;
         } 
    }

    private static void GetRandomNumber(int Capacity, List<int> numb , int _place)
    {
        for (int i = 0; i < _place ; i++)
        {
            int randNum = Random.Range(0, Capacity);
            if (!numb.Contains(randNum))
            {
                numb.Add(randNum);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cards;
    [SerializeField] private List<Transform> _cardPlace;

    bool _isStarted = false;

    public void AddCard(GameObject card)
    {
        _cards.Add(card);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isStarted)
        {
            SelectStarterCards.OnShowCard(_cards, _cardPlace);
            _isStarted = true;
        }
    }
}

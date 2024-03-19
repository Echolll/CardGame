using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CardPlayerService : MonoBehaviour
{
    [Inject]Player _currectPlayer;

    void Start()
    {
        if (_currectPlayer != null)
        {
            Debug.Log($"{_currectPlayer} - у меня инъекция этого игрока");
        }
    }

    public Player CurrectPlayer()
    {
        return _currectPlayer;
    }

    public class Factory : PlaceholderFactory<Player, CardPlayerService> { }
}
